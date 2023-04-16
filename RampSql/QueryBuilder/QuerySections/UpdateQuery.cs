using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class UpdateQuery : WhereSelector, IRampQuery
    {
        internal UpdateQuery(RampQueryData data) : base(data) { }

        public UpdateQuery Set(IRampColumn column, IRampFunction function) => Value(column, function, false);
        public UpdateQuery Set(IRampColumn column, object value) => Value(column, new RampConstant(value, null), true);
        public UpdateQuery Set(IRampColumn column, object value, bool parameterize) => Value(column, new RampConstant(value, null), parameterize);
        public UpdateQuery Set(IRampColumn column, IRampValue value, bool parameterize) => Value(column, value, parameterize);

        public UpdateQuery Value(IRampColumn column, IRampFunction function) => Value(column, function, false);
        public UpdateQuery Value(IRampColumn column, object value) => Value(column, new RampConstant(value, null), true);
        public UpdateQuery Value(IRampColumn column, object value, bool parameterize) => Value(column, new RampConstant(value, null), parameterize);
        public UpdateQuery Value(IRampColumn column, IRampValue value, bool parameterize)
        {
            data.Update.Add(new RampKVPElement(column, value, parameterize));
            return this;
        }
    }
}
