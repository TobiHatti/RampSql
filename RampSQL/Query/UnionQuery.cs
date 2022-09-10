namespace RampSQL.Query
{
    public class UnionQuery : WhereOrderSelector
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
