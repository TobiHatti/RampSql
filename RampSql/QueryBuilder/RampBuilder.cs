using System.Text;

namespace RampSql.QueryBuilder
{
    public static class RampBuilder
    {
        public static string Build(RampQueryData data)
        {
            StringBuilder query = new StringBuilder();
            //switch (data.OperationType)
            //{
            //    case OperationType.Select:
            //        query.Append("SELECT ");
            //        query.Append(SelectQuery());
            //        query.Append(JoinQuery());
            //        query.Append(WhereQuery());
            //        query.Append(GroupQuery());
            //        query.Append(HavingQuery());
            //        query.Append(OrderQuery());
            //        query.Append(LimitQuery());
            //        break;
            //    case OperationType.Insert:
            //        query.Append($"INSERT INTO {TargetTable} ");
            //        query.Append(InsertValuesQuery());
            //        query.Append(InsertResultQuery());
            //        break;
            //    case OperationType.Update:
            //        query.Append($"UPDATE {TargetTable} SET ");
            //        query.Append(UpdateValuesQuery());
            //        query.Append(WhereQuery());
            //        break;
            //    case OperationType.Delete:
            //        query.Append($"DELETE FROM {TargetTable} ");
            //        query.Append(WhereQuery());
            //        break;
            //    case OperationType.Union:
            //        query.Append(UnionQuery());
            //        query.Append(WhereQuery());
            //        query.Append(GroupQuery());
            //        query.Append(HavingQuery());
            //        query.Append(OrderQuery());
            //        query.Append(LimitQuery());
            //        break;
            //    case OperationType.Search:
            //        query.Append($"{SelectTargetTable} ");
            //        query.Append(JoinQuery());
            //        query.Append(WhereQuery());
            //        query.Append(GroupQuery());
            //        query.Append(HavingQuery());
            //        query.Append(OrderQuery());
            //        query.Append(LimitQuery());
            //        break;
            //    case OperationType.Upsert:
            //        throw new NotImplementedException("Upsert statements are not supported yet");
            //        break;
            //}

            return query.ToString();
        }
    }
}
