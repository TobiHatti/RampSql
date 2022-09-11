using RampSQL.Schema;

namespace RampSQL.Query
{
    public class HavingQuery : IQuerySection
    {
        private QueryData data;
        public HavingQuery(QueryData data) { this.data = data; }

        public HavingConnector DevProperty(RampColumn column, object value)
        {
            //data.WhereData.Add(new RampWhereData(column, value, WhereType.Is, LikeWildcard.Unspecified));
            return new HavingConnector(data);
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
    }
}
