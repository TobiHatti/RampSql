using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class WhereSelector : IRampQuery
    {
        protected RampQueryData data;
        internal WhereSelector(RampQueryData data) { this.data = data; }

        public WhereQuery<WhereConnector> Where
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public RampQueryData GetData() => data;
    }
}
