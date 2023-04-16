using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class HavingSelector : OrderQuery, IRampQuery
    {
        internal HavingSelector(RampQueryData data) : base(data) { }

        public HavingQuery Having
        {
            get
            {
                return new HavingQuery(data);
            }
        }
    }
}
