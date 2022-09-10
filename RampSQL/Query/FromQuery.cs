using RampSQL.Schema;

namespace RampSQL.Query
{
    public class FromQuery : IQuerySection
    {
        private IQuerySection parent;
        public FromQuery(IQuerySection parent) { this.parent = parent; }
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
