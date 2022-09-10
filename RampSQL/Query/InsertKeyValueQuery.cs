using RampSQL.Schema;

namespace RampSQL.Query
{
    public class InsertKeyValueQuery : InsertResultQuery, IQuerySection
    {
        public InsertKeyValueQuery Value(RampColumn column, object value)
        {
            return this;
        }
    }
}
