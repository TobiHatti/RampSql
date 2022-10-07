namespace RampSQL.Query
{
    public class WhereSelector : IQuerySection
    {
        protected QueryData data;
        public WhereSelector(QueryData data) { this.data = data; }

        public string AliasDeclaration { get => data.AliasDeclaration; }
        public string RealName { get => data.RealName; }
        public string AliasName { get => data.AliasName; }
        public void SetAlias(string alias) => data.SetAlias(alias);

        public WhereQuery<WhereConnector> Where
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IQuerySection Clone() => new QueryEngine((QueryData)data.Clone());
    }
}
