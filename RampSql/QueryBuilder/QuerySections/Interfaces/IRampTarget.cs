namespace RampSql.QueryBuilder
{
    public interface IRampTarget
    {
        public void AsAlias(string alias);
        public string AliasDeclaring { get; }
    }
}
