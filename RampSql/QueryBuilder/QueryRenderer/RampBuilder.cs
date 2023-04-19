using System.Runtime.InteropServices;

namespace RampSql.QueryBuilder
{
    public class RampBuilder
    {
        private RampQueryData data;
        private RampRenderEngine render;

        public RampBuilder(RampQueryData data)
        {
            this.data = data;
            render = new RampRenderEngine();
        }

        private void Preprocess()
        {
            if (data.SelectAll)
            {
                foreach (var item in data.columnCollection.Except(data.SelectValues))
                {
                    if (item.HasAlias) data.SelectValues.Add(item);

                }
            }

            if (data.OperationType == OperationType.Union)
            {
                foreach (IRampQuery query in data.Union)
                {
                    query.GetData().columnCollection.AddRange(data.columnCollection);
                }
            }
        }

        public object[] GetParameters()
        {
            return render.Parameters.ToArray();
        }

        public string GetQuery()
        {
            return render.Render();
        }

        public RampRenderEngine GetRenderer()
        {
            return render;
        }

        public RampBuilder Build()
        {
            Preprocess();


            switch (data.OperationType)
            {
                case OperationType.Select:
                    render.Instruction("SELECT");
                    SelectQuery();
                    JoinQuery();
                    WhereQuery();
                    GroupQuery();
                    HavingQuery();
                    OrderQuery();
                    LimitQuery();
                    break;
                case OperationType.Insert:
                    render.Instruction("INSERT INTO").Target(data.Target);
                    InsertValuesQuery();
                    InsertResultQuery();
                    break;
                case OperationType.Update:
                    render.Instruction("UPDATE").Target(data.Target).Instruction("SET");
                    UpdateValuesQuery();
                    WhereQuery();
                    break;
                case OperationType.Delete:
                    render.Instruction("DELETE FROM").Target(data.Target);
                    WhereQuery();
                    break;
                case OperationType.Union:
                    UnionQuery();
                    GroupQuery();
                    HavingQuery();
                    OrderQuery();
                    LimitQuery();
                    break;
                case OperationType.Search:
                    render.Target(data.Target);
                    JoinQuery();
                    WhereQuery();
                    GroupQuery();
                    HavingQuery();
                    OrderQuery();
                    LimitQuery();
                    break;
                case OperationType.Upsert:
                    throw new NotImplementedException("Upsert statements are not supported yet");
            }

            return this;
        }

        private void SelectQuery()
        {
            bool first = true;
            foreach (IRampValue val in CollectionsMarshal.AsSpan(data.SelectValues))
            {
                if (!first) render.Instruction(",");
                render.Value(val, RampRFormat.AliasDeclaring);
                first = false;
            }
            render.Instruction("FROM").Target(data.Target);
        }

        private void JoinQuery()
        {
            foreach (RampJoinElement join in CollectionsMarshal.AsSpan(data.Join))
            {
                switch (join.JoinType)
                {
                    case TableJoinType.Left: render.Instruction("LEFT JOIN"); break;
                    case TableJoinType.Right: render.Instruction("RIGHT JOIN"); break;
                    case TableJoinType.Inner: render.Instruction("INNER JOIN"); break;
                    case TableJoinType.FullOuter: render.Instruction("FULL OUTER JOIN"); break;
                }
                render
                    .Table(join.NewColumn.ParentTable, RampRFormat.AliasDeclaring, join.Alias)
                    .Instruction("ON")
                    .Column(join.ExistingColumn, RampRFormat.RealName)
                    .Instruction("=")
                    .Column(join.NewColumn, RampRFormat.RealName)
                ;
            }
        }

        private void WhereQuery()
        {
            if (data.Where.Count > 0)
            {
                render.Instruction("WHERE");
                foreach (var whereEntry in CollectionsMarshal.AsSpan(data.Where))
                {
                    if (whereEntry.GetType() == typeof(RampWhereElement))
                    {
                        RampWhereElement where = (RampWhereElement)whereEntry;
                        switch (where.WhereType)
                        {
                            case WhereType.SectionStart:
                                render.Instruction("(");
                                break;
                            case WhereType.Is:
                                render.Value(where.ColumnA, RampRFormat.AliasName).Instruction("=");
                                break;
                            case WhereType.IsNot:
                                render.Instruction("NOT").Value(where.ColumnA, RampRFormat.AliasName).Instruction("=");
                                break;
                            case WhereType.IsLike:
                                render.Value(where.ColumnA, RampRFormat.AliasName).Instruction("LIKE");
                                break;
                            case WhereType.IsNotLike:
                                render.Value(where.ColumnA, RampRFormat.AliasName).Instruction("NOT LIKE");
                                break;
                            case WhereType.In:
                                render.Value(where.ColumnA, RampRFormat.AliasName).Instruction("IN");
                                break;
                            case WhereType.IsNull:
                                render.Value(where.ColumnA, RampRFormat.AliasName).Instruction("IS NULL");
                                break;
                            case WhereType.IsNotNull:
                                render.Value(where.ColumnA, RampRFormat.AliasName).Instruction("IS NOT NULL");
                                break;
                        }

                        switch (where.LikeWildcard)
                        {
                            case LikeWildcard.MatchStart:
                                render.Instruction("CONCAT(").Value(where.ColumnB, where.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName).Instruction(",'%')");
                                break;
                            case LikeWildcard.MatchEnd:
                                render.Instruction("CONCAT('%',").Value(where.ColumnB, where.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName).Instruction(")");
                                break;
                            case LikeWildcard.MatchBoth:
                                render.Value(where.ColumnB, where.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName);
                                break;
                            case LikeWildcard.MatchAny:
                                render.Instruction("CONCAT('%',").Value(where.ColumnB, where.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName).Instruction(",'%')");
                                break;
                            case LikeWildcard.Unspecified:
                                if (where.ColumnB is IRampConstantArray)
                                {
                                    render.Instruction("(");
                                    for (int i = 0; i < where.ColumnB.GetParameterValues().Length; i++)
                                    {
                                        if (i != 0) render.Instruction(",");
                                        render.Instruction("?");
                                    }
                                    render.Instruction(")");
                                }
                                else render.Value(where.ColumnB, where.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName);
                                break;
                            case LikeWildcard.NoParameter:
                                break;
                        }
                    }
                    else
                    {
                        RampConditionConnector where = (RampConditionConnector)whereEntry;
                        switch (where.ConnectorType)
                        {
                            case ConditionConnectorType.And:
                                render.Instruction("AND");
                                break;
                            case ConditionConnectorType.Or:
                                render.Instruction("OR");
                                break;
                            case ConditionConnectorType.SectionEnd:
                                render.Instruction(")");
                                break;
                        }
                    }
                }
            }
        }

        private void GroupQuery()
        {
            if (data.GroupBy.Count > 0)
            {
                render.Instruction("GROUP BY");
                bool first = true;
                foreach (RampGroupElement group in CollectionsMarshal.AsSpan(data.GroupBy))
                {
                    if (!first) render.Instruction(",");
                    render.Value(group.Column, RampRFormat.AliasName);
                    first = false;
                }
            }
        }

        private void HavingQuery()
        {

        }

        private void OrderQuery()
        {
            if (data.Order.Count > 0)
            {
                render.Instruction("ORDER BY");
                bool first = true;
                foreach (RampOrderElement order in CollectionsMarshal.AsSpan(data.Order))
                {
                    if (!first) render.Instruction(",");
                    render.Value(order.Column, RampRFormat.AliasName);
                    switch (order.SortDirection)
                    {
                        case SortDirection.Ascending:
                            render.Instruction("ASC");
                            break;
                        case SortDirection.Descending:
                            render.Instruction("DESC");
                            break;
                    }
                    first = false;
                }
            }
        }

        private void LimitQuery()
        {
            if (data.SelectLimit != ulong.MaxValue || data.SelectOffset != 0)
            {
                render.Instruction("LIMIT").Raw(data.SelectOffset.ToString()).Instruction(",").Raw(data.SelectLimit.ToString());
            }
        }

        private void InsertValuesQuery()
        {
            render.Instruction("(");
            bool first = true;
            foreach (RampKVPElement entry in CollectionsMarshal.AsSpan(data.KVPairs))
            {
                if (!first) render.Instruction(",");
                render.Column(entry.Column, RampRFormat.AliasName);
                first = false;
            }

            render.Instruction(") VALUES (");
            first = true;
            foreach (RampKVPElement entry in CollectionsMarshal.AsSpan(data.KVPairs))
            {
                if (!first) render.Instruction(",");
                render.Value(entry.Value, entry.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName);
                first = false;
            }
            render.Instruction(")");
        }

        private void InsertResultQuery()
        {
            if (data.ReturnInsertID) render.Instruction("; SELECT LAST_INSERT_ID()");
        }

        private void UpdateValuesQuery()
        {
            bool first = true;
            foreach (RampKVPElement entry in CollectionsMarshal.AsSpan(data.KVPairs))
            {
                if (!first) render.Instruction(",");
                render.Column(entry.Column, RampRFormat.AliasName).Instruction("=").Value(entry.Value, entry.Parameterize ? RampRFormat.Parameter : RampRFormat.AliasName);
                first = false;
            }
        }

        private void UnionQuery()
        {
            bool first = true;
            foreach (IRampQuery query in data.Union)
            {
                if (!first)
                {
                    switch (data.UnionType)
                    {
                        case UnionType.Union: render.Instruction("UNION"); break;
                        case UnionType.UnionAll: render.Instruction("UNION ALL"); break;
                    }
                }
                render.Query(query);
                WhereQuery();
                first = false;
            }
        }
    }
}
