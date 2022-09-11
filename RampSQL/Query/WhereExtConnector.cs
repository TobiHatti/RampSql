namespace RampSQL.Query
{
    public class WhereExtConnector : GroupQuery, IQuerySection
    {

        public WhereExtConnector(QueryData data) : base(data) { }
        public WhereQuery<WhereExtConnector> And
        {
            get
            {
                data.WhereData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.And });
                return new WhereQuery<WhereExtConnector>(data);
            }
        }

        public WhereQuery<WhereExtConnector> Or
        {
            get
            {
                data.WhereData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.Or });
                return new WhereQuery<WhereExtConnector>(data);
            }
        }

        public WhereExtConnector SectEnd
        {
            get
            {
                data.WhereData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.SectionEnd });
                return this;
            }
        }
    }
}
