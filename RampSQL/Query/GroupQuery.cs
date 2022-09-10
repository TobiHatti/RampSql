using RampSQL.Schema;

namespace RampSQL.Query
{
    public class GroupQuery : HavingSelector
    {
        public GroupQuery GroupBy(RampColumn column)
        {
            return this;
        }
    }
}
