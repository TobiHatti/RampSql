using RampSql.QueryBuilder;

namespace RampSql.QuerySections
{
    public class InsertResultQuery : IRampQuery
    {
        protected RampQueryData data;
        internal InsertResultQuery(RampQueryData data) { this.data = data; }

        public InsertResultQuery GetLastID() { return this; }
    }
}
