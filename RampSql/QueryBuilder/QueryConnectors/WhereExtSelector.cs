using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class WhereExtSelector : GroupQuery, IRampQuery
    {
        public WhereQuery<WhereExtConnector> Where
        {
            get
            {
                return new WhereQuery<WhereExtConnector>(data);
            }
        }
    }
}
