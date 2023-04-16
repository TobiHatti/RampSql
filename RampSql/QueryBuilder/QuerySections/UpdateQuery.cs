using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class UpdateQuery : WhereSelector, IRampQuery
    {
        internal UpdateQuery(RampQueryData data) : base(data) { }

        public UpdateQuery Value(IRampColumn column, IRampFunction function) { return this; }
        public UpdateQuery Value(IRampColumn column, object value) { return this; }
        public UpdateQuery Value(IRampColumn column, object value, bool parameterize) { return this; }

        public UpdateQuery Set(IRampColumn column, IRampFunction function) { return this; }
        public UpdateQuery Set(IRampColumn column, object value) { return this; }
        public UpdateQuery Set(IRampColumn column, object value, bool parameterize) { return this; }
    }
}
