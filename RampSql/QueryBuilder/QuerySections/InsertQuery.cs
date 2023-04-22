using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class InsertQuery : InsertResultQuery, IRampQuery
    {
        internal InsertQuery(RampQueryData data) : base(data) { }

        public InsertQuery Value(IRampColumn column, IRampColumn value) => Value(column, value, false);
        public InsertQuery Value(IRampColumn column, IRampFunction function) => Value(column, function, false);
        public InsertQuery Value(IRampColumn column, IRampConstant value) => Value(column, value, true);
        public InsertQuery Value(IRampColumn column, object value) => Value(column, new RampConstant(value, null), true);
        public InsertQuery Value(IRampColumn column, object value, bool parameterize) => Value(column, new RampConstant(value, null), parameterize);

        public InsertQuery Value(IRampColumn column, IRampValue value, bool parameterize)
        {
            data.KVPairs.Add(new RampKVPElement(data, column, value, parameterize));
            return this;
        }

    }
}
