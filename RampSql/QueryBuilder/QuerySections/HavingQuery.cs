using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class HavingQuery : IRampQuery
    {
        protected RampQueryData data;
        internal HavingQuery(RampQueryData data) { this.data = data; }

        public HavingConnector DevProperty(IRampColumn column, object value)
        {
            return new HavingConnector(data);
        }

        public RampQueryData GetData() => data;
    }
}
