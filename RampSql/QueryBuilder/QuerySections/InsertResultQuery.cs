namespace RampSql.QueryBuilder
{
    public class InsertResultQuery : IRampQuery
    {
        protected RampQueryData data;
        internal InsertResultQuery(RampQueryData data) { this.data = data; }

        public InsertResultQuery GetLastID()
        {
            data.ReturnInsertID = true;
            return this;
        }


        public string RealName => null;
        public string QuotedSelectorName => null;
        public string AliasDeclaring => null;
        public bool HasAlias => !string.IsNullOrEmpty(data.QueryAlias);
        public RampQueryData GetData() => data;
        public void AsAlias(string alias) => data.QueryAlias = alias;
        public object[] GetParameterValues() => new object[0];

        public IRampQuery GetRampQuery() => this;
        public RampBuilder GetBuilder() => new RampBuilder(data);
        public string GetQuery() => GetBuilder().Build();
        public object[] GetParameters() => GetBuilder().GetParameters();
    }
}
