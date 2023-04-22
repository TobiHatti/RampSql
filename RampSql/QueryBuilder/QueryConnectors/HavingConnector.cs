namespace RampSql.QueryBuilder
{
    public class HavingConnector : OrderQuery, IRampQuery
    {
        internal HavingConnector(RampQueryData data) : base(data) { }

        public HavingQuery And
        {
            get
            {
                data.Having.Add(new RampConditionConnector(ConditionConnectorType.And));
                return new HavingQuery(data);
            }
        }

        public HavingQuery Or
        {
            get
            {
                data.Having.Add(new RampConditionConnector(ConditionConnectorType.Or));
                return new HavingQuery(data);
            }
        }
    }
}
