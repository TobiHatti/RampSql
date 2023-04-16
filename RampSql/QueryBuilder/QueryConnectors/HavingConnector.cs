using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class HavingConnector : OrderQuery, IRampQuery
    {
        internal HavingConnector(RampQueryData data) : base(data) { }

        public HavingQuery And
        {
            get
            {
                return new HavingQuery(data);
            }
        }

        public HavingQuery Or
        {
            get
            {
                return new HavingQuery(data);
            }
        }
    }
}
