namespace RampSQL.Query
{
    public class LimitQuery : IQuerySection
    {
        protected QueryData data;
        public LimitQuery(QueryData data) { this.data = data; }

        public string AliasDeclaration { get => data.AliasDeclaration; }
        public string RealName { get => data.RealName; }
        public string AliasName { get => data.AliasName; }
        public void SetAlias(string alias) => data.SetAlias(alias);
        public LimitQuery Limit(ulong limitCount)
        {
            data.SelectLimit = limitCount;
            return this;
        }

        public LimitQuery Limit(ulong limitCount, int offset)
        {
            data.SelectLimit = limitCount;
            data.SelectOffset = offset;
            return this;
        }

        public LimitQuery Shift(int offset)
        {
            data.SelectOffset = offset;
            return this;
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IQuerySection Clone() => new QueryEngine((QueryData)data.Clone());
    }
}
