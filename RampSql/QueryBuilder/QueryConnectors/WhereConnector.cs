namespace RampSql.QueryBuilder
{
    public class WhereConnector : QueryHead, IRampQuery
    {
        internal WhereConnector(RampQueryData data) : base(data) { }

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
    }
}
