using RampSQL.Schema;

namespace RampSQL.Query
{
    public class UpdateKeyValueQuery : WhereSelector, IQuerySection
    {
        public UpdateKeyValueQuery Value(RampColumn column, object value)
        {
            return this;
        }
    }
}
