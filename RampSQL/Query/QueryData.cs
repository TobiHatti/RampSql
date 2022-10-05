using RampSQL.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace RampSQL.Query
{
    public class QueryData : ICloneable
    {
        public List<IWhereQuerySegment> WhereData = new List<IWhereQuerySegment>();
        public List<IHavingQuerySegment> HavingData = new List<IHavingQuerySegment>();
        public List<RampParameterType> SelectColumns = new List<RampParameterType>();
        public List<RampParameterType> SelectValues = new List<RampParameterType>();
        public List<RampJoinData> Joins = new List<RampJoinData>();
        public List<RampColumn> GroupColumns = new List<RampColumn>();
        public List<RampKeyValuePair<RampColumn, SortDirection>> Orders = new List<RampKeyValuePair<RampColumn, SortDirection>>();
        public List<RampParameterType> ValuePairs = new List<RampParameterType>();
        public List<object> QueryParameters = new List<object>();
        public List<RampKeyValuePair<RampColumn, string>> CountColumns = new List<RampKeyValuePair<RampColumn, string>>();
        public List<RampUnionData> UnionQueries = new List<RampUnionData>();

        public UnionType UnionType = UnionType.UnionAll;
        public RampTable TargetTable = null;
        public OperationType QueryType = OperationType.Unknown;
        public bool SelectAll = false;
        public string SelectTargetTable = string.Empty;
        public string SelectTableAlias = string.Empty;
        public ulong SelectLimit;
        public int SelectOffset;
        public bool InsertReturnID = false;
        public bool SelectDistinct = false;

        public object Clone()
        {
            QueryData queryData = new QueryData();

            queryData.WhereData = new List<IWhereQuerySegment>();
            queryData.HavingData = new List<IHavingQuerySegment>();
            queryData.SelectColumns = new List<RampParameterType>();
            queryData.SelectValues = new List<RampParameterType>();
            queryData.Joins = new List<RampJoinData>();
            queryData.GroupColumns = new List<RampColumn>();
            queryData.Orders = new List<RampKeyValuePair<RampColumn, SortDirection>>();
            queryData.ValuePairs = new List<RampParameterType>();
            queryData.CountColumns = new List<RampKeyValuePair<RampColumn, string>>();
            queryData.UnionQueries = new List<RampUnionData>();

            WhereData.ForEach((item) => queryData.WhereData.Add((IWhereQuerySegment)(ICloneable)item.Clone()));
            HavingData.ForEach((item) => queryData.HavingData.Add((IHavingQuerySegment)(ICloneable)item.Clone()));
            SelectColumns.ForEach((item) => queryData.SelectColumns.Add((RampParameterType)(ICloneable)item.Clone()));
            SelectValues.ForEach((item) => queryData.SelectValues.Add((RampParameterType)(ICloneable)item.Clone()));
            Joins.ForEach((item) => queryData.Joins.Add((RampJoinData)(ICloneable)item.Clone()));
            GroupColumns.ForEach((item) => queryData.GroupColumns.Add((RampColumn)(ICloneable)item.Clone()));
            Orders.ForEach((item) => queryData.Orders.Add((RampKeyValuePair<RampColumn, SortDirection>)(ICloneable)item.Clone()));
            ValuePairs.ForEach((item) => queryData.ValuePairs.Add((RampParameterType)(ICloneable)item.Clone()));
            CountColumns.ForEach((item) => queryData.CountColumns.Add((RampKeyValuePair<RampColumn, string>)(ICloneable)item.Clone()));
            UnionQueries.ForEach((item) => queryData.UnionQueries.Add((RampUnionData)(ICloneable)item.Clone()));

            queryData.QueryParameters = new List<object>(QueryParameters);

            queryData.UnionType = UnionType;
            queryData.TargetTable = TargetTable;
            queryData.QueryType = QueryType;
            queryData.SelectAll = SelectAll;
            queryData.SelectTargetTable = SelectTargetTable;
            queryData.SelectTableAlias = SelectTableAlias;
            queryData.SelectLimit = SelectLimit;
            queryData.SelectOffset = SelectOffset;
            queryData.InsertReturnID = InsertReturnID;
            queryData.SelectDistinct = SelectDistinct;

            return queryData;
        }

        public object[] GetParameters() => QueryParameters.ToArray();

        public string RenderQuery()
        {
            StringBuilder query = new StringBuilder();
            switch (QueryType)
            {
                case OperationType.Select:
                    query.Append("SELECT ");
                    if (isDistinct) query.Append("DISTINCT ");
                    query.Append(SelectQuery());
                    query.Append(JoinQuery());
                    query.Append(WhereQuery());
                    query.Append(GroupQuery());
                    query.Append(HavingQuery());
                    query.Append(OrderQuery());
                    query.Append(LimitQuery());
                    break;
                case OperationType.Insert:
                    query.Append($"INSERT INTO {TargetTable} ");
                    query.Append(InsertValuesQuery());
                    query.Append(InsertResultQuery());
                    break;
                case OperationType.Update:
                    query.Append($"UPDATE {TargetTable} SET ");
                    query.Append(UpdateValuesQuery());
                    query.Append(WhereQuery());
                    break;
                case OperationType.Delete:
                    query.Append($"DELETE FROM {TargetTable} ");
                    query.Append(WhereQuery());
                    break;
                case OperationType.Union:
                    query.Append(UnionQuery());
                    query.Append(WhereQuery());
                    query.Append(GroupQuery());
                    query.Append(HavingQuery());
                    query.Append(OrderQuery());
                    query.Append(LimitQuery());
                    break;
                case OperationType.Search:
                    query.Append($"{SelectTargetTable} ");
                    query.Append(JoinQuery());
                    query.Append(WhereQuery());
                    query.Append(GroupQuery());
                    query.Append(HavingQuery());
                    query.Append(OrderQuery());
                    query.Append(LimitQuery());
                    break;
                case OperationType.Upsert:
                    throw new NotImplementedException("Upsert statements are not supported yet");
                    break;
            }

            return query.ToString();
        }

        private string SelectQuery()
        {
            StringBuilder query = new StringBuilder();

            if (SelectAll) query.Append("* ");

            bool first = !SelectAll;
            foreach (var column in SelectColumns)
            {
                if (!first) query.Append(", ");
                query.Append(column.Column);

                if (!string.IsNullOrEmpty(column.Alias)) query.Append($" AS {column.Alias}");
                else if (column.Column.requiresAlias) query.Append($" AS {column.Column.columnAlias}");
                first = false;
            }

            first = !SelectAll && SelectColumns.Count == 0;
            foreach (var value in SelectValues)
            {
                if (!first) query.Append(", ");
                if (value.Parameterized)
                {
                    query.Append("? ");
                    QueryParameters.Add(value.ParamColumn);
                }
                else query.Append($"{value.ParamColumn} ");

                if (!string.IsNullOrEmpty(value.Alias)) query.Append($" AS {value.Alias}");
                first = false;
            }

            first = !SelectAll && SelectColumns.Count == 0 && SelectValues.Count == 0;
            foreach (var count in CountColumns)
            {
                if (!first) query.Append(", ");
                if (count.Key == null) query.Append("COUNT(*) ");
                else query.Append($"COUNT({count.Key}) ");
                if (!string.IsNullOrEmpty(count.Value)) query.Append($"AS {count.Value}");
                first = false;
            }

            query.Append($" FROM {SelectTargetTable} ");
            if (!string.IsNullOrEmpty(SelectTableAlias)) query.Append($"AS {SelectTableAlias} ");

            return query.ToString();
        }

        private string JoinQuery()
        {
            StringBuilder query = new StringBuilder();
            foreach (var join in Joins)
            {
                switch (join.Type)
                {
                    case TableJoinType.Left:
                        query.Append("LEFT JOIN ");
                        break;
                    case TableJoinType.Right:
                        query.Append("RIGHT JOIN ");
                        break;
                    case TableJoinType.Inner:
                        query.Append("INNER JOIN ");
                        break;
                    case TableJoinType.FullOuter:
                        query.Append("FULL OUTER JOIN ");
                        break;
                }
                query.Append($"{join.NewTableColumn.ParentTable} ");
                if (!string.IsNullOrEmpty(join.Alias))
                {
                    query.Append($"AS {join.Alias} ");
                    query.Append($"ON {join.ExistingTableColumn} = `{join.Alias}`.{join.NewTableColumn.QCN} ");
                }
                else query.Append($"ON {join.ExistingTableColumn} = {join.NewTableColumn} ");
            }
            return query.ToString();
        }

        private string WhereQuery()
        {
            if (WhereData.Count > 0)
            {
                StringBuilder query = new StringBuilder();
                query.Append("WHERE ");
                foreach (var whereEntry in WhereData)
                {
                    if (whereEntry.GetType() == typeof(RampWhereData))
                    {
                        RampWhereData where = whereEntry as RampWhereData;
                        switch (where.WhereType)
                        {
                            case WhereType.SectionStart:
                                query.Append("(");
                                break;
                            case WhereType.Is:
                                query.Append($"{where.Column} = ");
                                break;
                            case WhereType.IsNot:
                                query.Append($"NOT {where.Column} = ");
                                break;
                            case WhereType.IsLike:
                                query.Append($"{where.Column} LIKE ");
                                break;
                            case WhereType.IsNotLike:
                                query.Append($"{where.Column} NOT LIKE ");
                                break;
                            case WhereType.In:
                                query.Append($"{where.Column} IN ");
                                break;
                            case WhereType.IsNull:
                                query.Append($"{where.Column} IS NULL ");
                                break;
                            case WhereType.IsNotNull:
                                query.Append($"{where.Column} IS NOT NULL ");
                                break;
                        }

                        switch (where.LikeWildcard)
                        {
                            case LikeWildcard.MatchStart:
                                query.Append("CONCAT(?,'%') ");
                                break;
                            case LikeWildcard.MatchEnd:
                                query.Append("CONCAT('%',?) ");
                                break;
                            case LikeWildcard.MatchBoth:
                                query.Append("? ");
                                break;
                            case LikeWildcard.MatchAny:
                                query.Append("CONCAT('%',?,'%') ");
                                break;
                            case LikeWildcard.Unspecified:
                                if (where.Values.Length > 1)
                                {
                                    query.Append("(");
                                    bool first = true;
                                    foreach (object o in where.Values)
                                    {
                                        if (!first) query.Append(", ");
                                        query.Append("?");
                                        first = false;
                                    }
                                    query.Append(") ");
                                }
                                else
                                {
                                    if (where.Parameterize) query.Append("? ");
                                    else
                                    {
                                        if (where.Values[0].GetType().GetInterface(nameof(IQuerySection)) != null)
                                        {
                                            IQuerySection qs = (IQuerySection)where.Values[0];
                                            query.Append($"({qs}) ");
                                            QueryParameters.AddRange(qs.GetParameters());
                                        }
                                        else
                                        {
                                            query.Append($"{where.Values[0]} ");
                                        }
                                    }
                                }
                                break;
                            case LikeWildcard.NoParameter:
                                break;
                        }

                        if (where.Parameterize) QueryParameters.AddRange(where.Values);
                    }
                    else
                    {
                        RampConditionConnector where = whereEntry as RampConditionConnector;
                        switch (where.ConnectorType)
                        {
                            case ConditionConnectorType.And:
                                query.Append("AND ");
                                break;
                            case ConditionConnectorType.Or:
                                query.Append("OR ");
                                break;
                            case ConditionConnectorType.SectionEnd:
                                query.Append(") ");
                                break;
                        }
                    }
                }
                return query.ToString();
            }
            return string.Empty;
        }

        private string GroupQuery()
        {
            if (GroupColumns.Count > 0)
            {
                StringBuilder query = new StringBuilder();
                query.Append("GROUP BY ");
                bool first = true;
                foreach (RampColumn column in GroupColumns)
                {
                    if (!first) query.Append(", ");
                    query.Append(column);
                    first = false;
                }
                return query.ToString();
            }
            return string.Empty;
        }

        private string HavingQuery()
        {
            return string.Empty;
        }

        private string OrderQuery()
        {
            if (Orders.Count > 0)
            {
                StringBuilder query = new StringBuilder();
                query.Append("ORDER BY ");
                bool first = true;
                foreach (var order in Orders)
                {
                    if (!first) query.Append(", ");
                    query.Append(order.Key);
                    switch (order.Value)
                    {
                        case SortDirection.Ascending:
                            query.Append(" ASC ");
                            break;
                        case SortDirection.Descending:
                            query.Append(" DESC ");
                            break;
                    }
                    first = false;
                }
                return query.ToString();
            }
            return string.Empty;
        }

        private string LimitQuery()
        {
            if (SelectLimit != 0 || SelectOffset != 0)
            {
                StringBuilder query = new StringBuilder();

                // Max bigint for mysql. ugly but works
                if (SelectLimit == 0) SelectLimit = 18446744073709551615;

                query.Append($"LIMIT {SelectOffset},{SelectLimit}");

                return query.ToString();
            }

            return string.Empty;
        }

        private string InsertValuesQuery()
        {
            StringBuilder columnQuery = new StringBuilder();
            StringBuilder paramQuery = new StringBuilder();
            bool first = true;
            foreach (var entry in ValuePairs)
            {
                if (!first)
                {
                    columnQuery.Append(", ");
                    paramQuery.Append(", ");
                }
                columnQuery.Append($"{entry.Column} ");
                if (entry.Parameterized)
                {
                    paramQuery.Append($"? ");
                    QueryParameters.Add(entry.Value);
                }
                else paramQuery.Append($"{entry.Value} ");

                first = false;
            }

            return $"({columnQuery}) VALUES ({paramQuery}) ";
        }

        private string UpdateValuesQuery()
        {
            StringBuilder query = new StringBuilder();
            bool first = true;
            foreach (var entry in ValuePairs)
            {
                if (!first) query.Append(", ");

                if (entry.Parameterized)
                {
                    query.Append($"{entry.Column} = ? ");
                    QueryParameters.Add(entry.Value);
                }
                else query.Append($"{entry.Column} = {entry.Value} ");

                first = false;
            }

            return query.ToString();
        }

        private string InsertResultQuery()
        {
            if (InsertReturnID) return "; SELECT LAST_INSERT_ID();";
            return string.Empty;
        }

        private string UnionQuery()
        {
            StringBuilder query = new StringBuilder();
            bool first = true;
            foreach (RampUnionData union in UnionQueries)
            {
                if (!first)
                {
                    switch (UnionType)
                    {
                        case UnionType.UnionAll:
                            query.Append("UNION ALL ");
                            break;
                        case UnionType.Union:
                            query.Append("UNION ");
                            break;
                    }

                }
                if (!string.IsNullOrEmpty(union.Alias)) query.Append($"({union.SubQuery}) AS {union.Alias} ");
                else query.Append($"({union.SubQuery}) ");
                QueryParameters.AddRange(union.Parameters);
                first = false;
            }
            return query.ToString();
        }


    }
}
