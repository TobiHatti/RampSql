using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class WhereSelector : IRampQuery
    {
        public WhereQuery<WhereConnector> Where
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }

    }
}
