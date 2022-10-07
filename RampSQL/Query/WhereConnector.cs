namespace RampSQL.Query
{
    public class WhereConnector : IQuerySection
    {
        private QueryData data;
        public WhereConnector(QueryData data) { this.data = data; }

        public string AliasDeclaration { get => data.AliasDeclaration; }
        public string RealName { get => data.RealName; }
        public string AliasName { get => data.AliasName; }
        public void SetAlias(string alias) => data.SetAlias(alias);
        public WhereQuery<WhereConnector> And
        {
            get
            {
                data.WhereData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.And });
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public WhereQuery<WhereConnector> Or
        {
            get
            {
                data.WhereData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.Or });
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public WhereConnector SectEnd
        {
            get
            {
                data.WhereData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.SectionEnd });
                return this;
            }
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IQuerySection Clone() => new QueryEngine((QueryData)data.Clone());
    }
}
