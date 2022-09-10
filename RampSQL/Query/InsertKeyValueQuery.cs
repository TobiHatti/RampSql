using RampSQL.Schema;

namespace RampSQL.Query
{
    public class InsertKeyValueQuery : InsertResultQuery, IQuerySection
    {
        public InsertKeyValueQuery(IQuerySection parent) : base(parent) { }
        public InsertKeyValueQuery Value(RampColumn column, object value)
        {
            return this;
        }
    }
}
