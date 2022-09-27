namespace RampSQL.Query
{
    public class InsertResultQuery : IQuerySection
    {
        public QueryData data;
        public InsertResultQuery(QueryData data) { this.data = data; }
        public InsertResultQuery GetLastID()
        {
            data.InsertReturnID = true;
            return this;
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IRampQuery Clone() => new QueryEngine((QueryData)data.Clone());
    }
}
