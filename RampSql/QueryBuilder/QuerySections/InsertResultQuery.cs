using RampSql.QueryBuilder;

namespace RampSql.QuerySections
{
    public class InsertResultQuery : IRampQuery
    {
        public InsertResultQuery GetLastID() { return this; }
    }
}
