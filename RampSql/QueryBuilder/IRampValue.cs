namespace RampSql.QueryBuilder
{
    public interface IRampValue
    {
        public string RealName { get; }
        public string QuotedSelectorName { get; }
        public string AliasDeclaring { get; }
        public bool HasAlias { get; }
        public void AsAlias(string alias);
        public object[] GetParameterValues();
    }
}
