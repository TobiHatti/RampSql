namespace RampSQL.Query
{
    public class WhereConnector : IQuerySection
    {
        private QueryData data;
        public WhereConnector(QueryData data) { this.data = data; }
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
    }
}
