namespace RampSQL.Query
{
    public class HavingConnector : OrderQuery, IQuerySection
    {
        public HavingConnector(QueryData data) : base(data) { }
        public HavingQuery And
        {
            get
            {
                data.HavingData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.And });
                return new HavingQuery(data);
            }
        }

        public HavingQuery Or
        {
            get
            {
                data.HavingData.Add(new RampConditionConnector() { ConnectorType = ConditionConnectorType.Or });
                return new HavingQuery(data);
            }
        }
    }
}
