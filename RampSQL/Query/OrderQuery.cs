using RampSQL.Schema;

namespace RampSQL.Query
{
    public class OrderQuery : LimitQuery, IQuerySection
    {
        public OrderQuery(IQuerySection parent) : base(parent) { }
        public OrderQuery OrderBy(RampColumn column, SortDirection direction)
        {
            return this;
        }
    }
}
