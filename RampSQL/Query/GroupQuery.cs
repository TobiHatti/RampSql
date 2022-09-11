using RampSQL.Schema;

namespace RampSQL.Query
{
    public class GroupQuery : HavingSelector, IQuerySection
    {
        public GroupQuery(QueryData data) : base(data) { }
        public GroupQuery GroupBy(RampColumn column)
        {
            data.GroupColumns.Add(column);
            return this;
        }
    }
}
