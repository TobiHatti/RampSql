using RampSQL.Schema;

namespace RampSQL.Query
{
    public class FromQuery : IQuerySection
    {
        public JoinQuery From(RampTable table)
        {
            return null;
        }
        public JoinQuery From(QueryEngine subQuery)
        {
            return null;
        }
        public JoinQuery From(string query)
        {
            return null;
        }
    }
}
