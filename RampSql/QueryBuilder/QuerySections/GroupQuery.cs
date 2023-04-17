using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
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
