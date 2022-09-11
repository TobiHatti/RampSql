using RampSQL.Schema;
using System.Collections.Generic;

namespace RampSQL.Query
{
    public class InsertKeyValueQuery : InsertResultQuery, IQuerySection
    {
        public InsertKeyValueQuery(QueryData data) : base(data) { }
        public InsertKeyValueQuery Value(RampColumn column, object value)
        {
            data.ValuePairs.Add(new KeyValuePair<RampColumn, object>(column, value));
            return this;
        }
    }
}
