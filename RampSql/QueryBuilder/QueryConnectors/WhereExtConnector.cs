namespace RampSql.QueryBuilder
{
    public class WhereExtConnector : GroupQuery, IRampQuery
    {
        internal WhereExtConnector(RampQueryData data) : base(data) { }

        public WhereQuery<WhereExtConnector> And
        {
            get
            {
                data.Where.Add(new RampConditionConnector(ConditionConnectorType.And));
                return new WhereQuery<WhereExtConnector>(data);
            }
        }

        public WhereQuery<WhereExtConnector> Or
        {
            get
            {
                data.Where.Add(new RampConditionConnector(ConditionConnectorType.Or));
                return new WhereQuery<WhereExtConnector>(data);
            }
        }

        public WhereExtConnector SectEnd
        {
            get
            {
                data.Where.Add(new RampConditionConnector(ConditionConnectorType.SectionEnd));
                return this;
            }
        }
    }
}
