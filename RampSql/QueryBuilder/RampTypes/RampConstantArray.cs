namespace RampSql.QueryBuilder
{
    public class RampConstantArray : IRampConstantArray
    {
        public object[] Values { get; set; }

        public RampConstantArray(params object[] values)
        {
            Values = values;
        }

        public string RealName => string.Empty;
        public string QuotedSelectorName { get; }
        public string AliasDeclaring { get; }
        public void AsAlias(string alias) { }
        public bool HasAlias => false;
        public object[] GetParameterValues() => Values;
    }
}
