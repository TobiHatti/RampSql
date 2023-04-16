using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class WhereExtSelector : GroupQuery, IRampQuery
    {
        internal WhereExtSelector(RampQueryData data) : base(data) { }

        public WhereQuery<WhereExtConnector> Where
        {
            get
            {
                return new WhereQuery<WhereExtConnector>(data);
            }
        }
    }
}
