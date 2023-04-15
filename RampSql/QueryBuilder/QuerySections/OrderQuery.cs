using RampSql.QueryBuilder;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class OrderQuery : LimitQuery, IRampQuery
    {
        public OrderQuery OrderBy(IRampColumn column, SortDirection direction) { return this; }
    }
}
