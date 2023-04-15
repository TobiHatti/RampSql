using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class HavingSelector : OrderQuery, IRampQuery
    {
        public HavingQuery Having
        {
            get
            {
                return new HavingQuery(data);
            }
        }
    }
}
