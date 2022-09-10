using RampSQL.Schema;

namespace RampSQL.Query
{
    public class SelectQuery : FromQuery
    {
        public SelectQuery All()
        {
            return this;
        }

        public SelectQuery Column(RampColumn column)
        {
            return this;
        }

        public SelectQuery Column(RampColumn column, string alias)
        {
            return this;
        }

        public SelectQuery Columns(params RampColumn[] columns)
        {
            return this;
        }

        public SelectQuery Value(object value, string alias)
        {
            return this;
        }
    }
}
