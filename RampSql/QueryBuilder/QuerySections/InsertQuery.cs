using RampSql.QueryBuilder;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class InsertQuery : InsertResultQuery, IRampQuery
    {
        public InsertQuery Value(IRampColumn column, IRampFunction function) { return this; }
        public InsertQuery Value(IRampColumn column, object value) { return this; }
        public InsertQuery Value(IRampColumn column, object value, bool parameterize) { return this; }
    }
}
