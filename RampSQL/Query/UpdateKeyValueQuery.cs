using RampSQL.Schema;

namespace RampSQL.Query
{
    public class UpdateKeyValueQuery : WhereSelector
    {
        public UpdateKeyValueQuery Value(RampColumn column, object value)
        {
            return this;
        }
    }
}
