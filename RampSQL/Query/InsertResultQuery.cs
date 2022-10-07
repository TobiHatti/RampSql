namespace RampSQL.Query
{
    public class InsertResultQuery : IQuerySection
    {
        public QueryData data;
        public InsertResultQuery(QueryData data) { this.data = data; }
        public string AliasDeclaration { get => data.AliasDeclaration; }
        public string RealName { get => data.RealName; }
        public string AliasName { get => data.AliasName; }
        public void SetAlias(string alias) => data.SetAlias(alias);

        public InsertResultQuery GetLastID()
        {
            data.InsertReturnID = true;
            return this;
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IQuerySection Clone() => new QueryEngine((QueryData)data.Clone());
    }
}
