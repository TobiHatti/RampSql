using RampSQL.Schema;
using System.Collections.Generic;
using System.Text;

namespace RampSQL.Query
{
    public class QueryData
    {
        public List<IWhereQuerySegment> WhereData = new List<IWhereQuerySegment>();
        public List<IHavingQuerySegment> HavingData = new List<IHavingQuerySegment>();
        public List<KeyValuePair<RampColumn, string>> SelectColumns = new List<KeyValuePair<RampColumn, string>>();
        public List<KeyValuePair<object, string>> SelectValues = new List<KeyValuePair<object, string>>();
        public List<RampJoinData> Joins = new List<RampJoinData>();
        public List<RampColumn> GroupColumns = new List<RampColumn>();
        public List<KeyValuePair<RampColumn, SortDirection>> Orders = new List<KeyValuePair<RampColumn, SortDirection>>();
        public List<KeyValuePair<RampColumn, object>> ValuePairs = new List<KeyValuePair<RampColumn, object>>();
        public List<object> QueryParameters = new List<object>();
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

        public object[] GetParameters() => QueryParameters.ToArray();

        public string RenderQuery()
        {
            StringBuilder query = new StringBuilder();
            switch (QueryType)
            {
                case OperationType.Select:
                    query.Append("SELECT ");
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
                    query.Append($"UPDATE {TargetTable} ");
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
                query.Append(column.Key);
                if (!string.IsNullOrEmpty(column.Value)) query.Append($" AS {column.Value}");
                first = false;
            }

            first = !SelectAll && SelectColumns.Count == 0;
            foreach (var value in SelectValues)
            {
                if (!first) query.Append(", ");
                QueryParameters.Add(value.Key);
                if (!string.IsNullOrEmpty(value.Value)) query.Append($" ? AS {value.Value}");
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
                }
                query.Append($"{join.NewTableColumn.ParentTable} ON {join.ExistingTableColumn} = {join.NewTableColumn} ");
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
                                query.Append("IN (");
                                bool first = true;
                                foreach (object o in where.Values)
                                {
                                    if (!first) query.Append(", ");
                                    query.Append("?");
                                    first = false;
                                }
                                query.Append(") ");
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
                                query.Append("? ");
                                break;
                        }

                        QueryParameters.AddRange(where.Values);
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
            foreach (KeyValuePair<RampColumn, object> entry in ValuePairs)
            {
                if (!first)
                {
                    columnQuery.Append(", ");
                    paramQuery.Append(", ");
                }
                columnQuery.Append($"{entry.Key} ");
                paramQuery.Append($"? ");
                QueryParameters.Add(entry.Value);
                first = false;
            }

            return $"({columnQuery}) VALUES ({paramQuery}) ";
        }

        private string UpdateValuesQuery()
        {
            StringBuilder query = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<RampColumn, object> entry in ValuePairs)
            {
                if (!first) query.Append(", ");
                query.Append($"{entry.Key} = ? ");
                QueryParameters.Add(entry.Value);
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
