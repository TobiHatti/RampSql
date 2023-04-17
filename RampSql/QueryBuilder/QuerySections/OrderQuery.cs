using RampSql.QueryBuilder;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class OrderQuery : LimitQuery, IRampQuery
    {
        internal OrderQuery(RampQueryData data) : base(data) { }

        public OrderQuery OrderBy(IRampColumn column, SortDirection direction)
        {
            data.Order.Add(new RampOrderElement(data, column, direction));
            return this;
        }
    }
}
