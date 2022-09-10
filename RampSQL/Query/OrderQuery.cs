using RampSQL.Schema;

namespace RampSQL.Query
{
    public class OrderQuery : LimitQuery
    {
        public OrderQuery OrderBy(RampColumn column, SortDirection direction)
        {
            return this;
        }
    }
}
