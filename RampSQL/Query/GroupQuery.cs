using RampSQL.Schema;

namespace RampSQL.Query
{
    public class GroupQuery : HavingSelector, IQuerySection
    {
        public GroupQuery(IQuerySection parent) : base(parent) { }
        public GroupQuery GroupBy(RampColumn column)
        {
            return this;
        }
    }
}
