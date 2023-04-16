using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class WhereExtConnector : GroupQuery, IRampQuery
    {
        internal WhereExtConnector(RampQueryData data) : base(data) { }

        public WhereQuery<WhereExtConnector> And
        {
            get
            {
                return new WhereQuery<WhereExtConnector>(data);
            }
        }

        public WhereQuery<WhereExtConnector> Or
        {
            get
            {
                return new WhereQuery<WhereExtConnector>(data);
            }
        }

        public WhereExtConnector SectEnd
        {
            get
            {
                return this;
            }
        }
    }
}
