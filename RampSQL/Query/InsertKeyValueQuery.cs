using RampSQL.Schema;

namespace RampSQL.Query
{
    public class InsertKeyValueQuery : InsertResultQuery, IQuerySection
    {
        public InsertKeyValueQuery(QueryData data) : base(data) { }
        public InsertKeyValueQuery Value(RampColumn column, SqlFunction function, params object[] parameters) => Value(column, new QueryFunc(function, null, parameters), false);
        public InsertKeyValueQuery Value(RampColumn column, MySqlFunctions function, params object[] parameters) => Value(column, new QueryFunc(function, null, parameters), false);
        public InsertKeyValueQuery Value(RampColumn column, object value) => Value(column, value, true);
        public InsertKeyValueQuery Value(RampColumn column, object value, bool parameterize)
        {
            data.ValuePairs.Add(new RampParameterType(column, value, parameterize));
            return this;
        }
    }
}
