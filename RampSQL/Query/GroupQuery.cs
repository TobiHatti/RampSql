using RampSQL.Schema;

namespace RampSQL.Query
{
    public class GroupQuery : HavingSelector, IQuerySection
    {
        public GroupQuery GroupBy(RampColumn column)
        {
            return this;
        }
    }
}
