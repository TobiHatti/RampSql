namespace RampSQL.Query
{
    public class UnionQuery : WhereExtSelector, IQuerySection
    {
        public UnionQuery(QueryData data) : base(data) { }
        public UnionQuery SubQuery(IQuerySection subQuery) => SubQuery(subQuery.ToString(), null, subQuery.GetParameters());
        public UnionQuery SubQuery(IQuerySection subQuery, string alias) => SubQuery(subQuery.ToString(), alias, subQuery.GetParameters());

        public UnionQuery SubQuery(string subQuery, string alias, params object[] parameters)
        {
            data.UnionQueries.Add(new RampUnionData(subQuery, alias, parameters));
            return this;
        }
    }
}
