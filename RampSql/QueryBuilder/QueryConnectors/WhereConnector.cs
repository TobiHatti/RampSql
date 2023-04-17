namespace RampSql.QueryBuilder
{
    public class WhereConnector : IRampQuery
    {
        protected RampQueryData data;
        internal WhereConnector(RampQueryData data) { this.data = data; }

        public WhereQuery<WhereConnector> And
        {
            get
            {
                data.Where.Add(new RampConditionConnector(ConditionConnectorType.And));
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public WhereQuery<WhereConnector> Or
        {
            get
            {
                data.Where.Add(new RampConditionConnector(ConditionConnectorType.Or));
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public WhereConnector SectEnd
        {
            get
            {
                data.Where.Add(new RampConditionConnector(ConditionConnectorType.SectionEnd));
                return this;
            }
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
