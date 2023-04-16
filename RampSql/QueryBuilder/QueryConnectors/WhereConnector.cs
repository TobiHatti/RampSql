using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
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

        public RampQueryData GetData() => data;
    }
}
