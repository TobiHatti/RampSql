using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class GroupQuery : HavingSelector, IRampQuery
    {
        internal GroupQuery(RampQueryData data) : base(data) { }

        public GroupQuery GroupBy(IRampColumn column)
        {
            data.GroupBy.Add(new RampGroupElement(data, column));
            return this;
        }
    }
}
