using RampSQL.Schema;

namespace RampSQL.Query
{
    public class InsertKeyValueQuery : InsertResultQuery
    {
        public InsertKeyValueQuery Value(RampColumn column, object value)
        {
            return this;
        }
    }
}
