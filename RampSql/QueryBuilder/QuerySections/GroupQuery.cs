using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class GroupQuery : HavingSelector, IRampQuery
    {
        public GroupQuery GroupBy(IRampColumn column) { return this; }
    }
}
