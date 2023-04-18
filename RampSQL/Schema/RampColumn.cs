namespace RampSql.Schema
{
    public class RampColumn : IRampColumn
    {
        public string DBColumnName { get; }
        public Type Type { get; }
        public RampTableData ParentTable { get; }
        private string alias = string.Empty;
        public string Alias
        {
            get => alias;
            set
            {
                if (!string.IsNullOrEmpty(value)) alias = value;
            }
        }

        public RampColumn(RampTableData parentTable, string dBColumnName, Type type, string alias)
        {
            DBColumnName = dBColumnName;
            Type = type;
            ParentTable = parentTable;
            Alias = alias;
        }

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public string FQN { get => $"{ParentTable.QuotedSelectorName}.`{DBColumnName}`"; }

        /// <summary>
        /// Return the quoted column name ("`column`")
        /// </summary>
        public string QCN { get => $"`{DBColumnName}`"; }

        /// <summary>
        /// Return the unquoted column name ("column")
        /// </summary>
        public string UCN { get => DBColumnName; }

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public override string ToString() => FQN;

        public void AsAlias(string alias)
        {
            Alias = alias;
        }

        public string RealQuotedName => FQN;
        public string RealName => UCN;
        public string QuotedSelectorName
        {
            get
            {
                if (string.IsNullOrEmpty(Alias)) return FQN;
                else return $"`{Alias}`";
            }
        }
        public string AliasDeclaring
        {
            get
            {
                if (string.IsNullOrEmpty(Alias)) return FQN;
                else return $"{FQN} AS {QuotedSelectorName}";
            }
        }

        public bool HasAlias => !string.IsNullOrEmpty(Alias);
        public object[] GetParameterValues() => new object[0];
    }
}
