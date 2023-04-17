namespace RampSql.QueryBuilder
{
    public class WhereSelector : IRampQuery
    {
        protected RampQueryData data;
        internal WhereSelector(RampQueryData data) { this.data = data; }

        public WhereQuery<WhereConnector> Where
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public string RealName => null;
        public string QuotedSelectorName => null;
        public string AliasDeclaring => null;
        public bool HasAlias => !string.IsNullOrEmpty(data.QueryAlias);
        public RampQueryData GetData() => data;
        public void AsAlias(string alias) => data.QueryAlias = alias;
        public object[] GetParameterValues() => new object[0];
    }
}
