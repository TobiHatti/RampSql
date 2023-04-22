namespace RampSql.QueryBuilder
{
    public class RampConstant : IRampConstant
    {
        public object Value { get; set; }
        private string alias = string.Empty;
        public string Alias
        {
            get => alias;
            set
            {
                if (!string.IsNullOrEmpty(value)) alias = value;
            }
        }

        public RampConstant(object value, string alias)
        {
            Value = value;
            Alias = alias;
        }

        public string RealName => Value.ToString();
        public string QuotedSelectorName
        {
            get
            {
                if (string.IsNullOrEmpty(Alias)) return RealName;
                else return $"`{Alias}`";
            }
        }
        public string AliasDeclaring
        {
            get
            {
                if (string.IsNullOrEmpty(Alias)) return RealName;
                else return $"{RealName} AS {QuotedSelectorName}";
            }
        }

        public void AsAlias(string alias)
        {
            Alias = alias;
        }

        public bool HasAlias => !string.IsNullOrEmpty(Alias);
        public object[] GetParameterValues() => new object[] { Value };
    }
}
