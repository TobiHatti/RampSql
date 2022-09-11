using RampSQL.Schema;
using System.Collections.Generic;

namespace RampSQL.Query
{
    public class OrderQuery : LimitQuery, IQuerySection
    {
        public OrderQuery(QueryData data) : base(data) { }
        public OrderQuery OrderBy(RampColumn column, SortDirection direction)
        {
            data.Orders.Add(new KeyValuePair<RampColumn, SortDirection>(column, direction));
            return this;
        }
    }
}
