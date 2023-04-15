using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class HavingConnector : OrderQuery, IRampQuery
    {
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
