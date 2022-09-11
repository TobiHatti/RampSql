using RampSQL.Schema;
using System.Collections.Generic;

namespace RampSQL.Query
{
    public class UpdateKeyValueQuery : WhereSelector, IQuerySection
    {
        public UpdateKeyValueQuery(QueryData data) : base(data) { }
        public UpdateKeyValueQuery Value(RampColumn column, object value)
        {
            data.ValuePairs.Add(new KeyValuePair<RampColumn, object>(column, value));
            return this;
        }
    }
}
