namespace RampSQL.Query
{
    public class UnionQuery : WhereExtSelector, IQuerySection
    {
        public UnionQuery SubQuery(QueryEngine subQuery, string alias)
        {
            return this;
        }

        public UnionQuery SubQuery(string subQuery, string alias)
        {
            return this;
        }
    }
}


// GROUP BYYYYY

// select from join where groupby having order limit