using RampSql.QueryBuilder;

namespace RampSql.Schema
{
    public class RampTableData : IRampTable, IRampTarget
    {
        public string DBTableName { get; }
        public Type Type { get; }
        public RampColumn[] Columns { get; set; }
        public RampSchemaData ParentSchema { get; }
        private string alias = string.Empty;
        public string Alias
        {
            get => alias;
            set
            {
                if (!string.IsNullOrEmpty(value)) alias = value;
            }
        }
        public RampTableData(RampSchemaData schema, string dBTableName, Type type)
        {
            ParentSchema = schema;
            DBTableName = dBTableName;
            Type = type;
        }


        public string QuotedBackCall { get => $"`{ParentSchema.Alias}`"; }
        public string RealQuotedTableName { get => $"`{DBTableName}`"; }
        public string QuotedTableName
        {
            get
            {
                if (ParentSchema.IsBackCall) return QuotedBackCall;
                else return $"`{ParentSchema.SubQueryPrefix}{DBTableName}`";
            }
        }
        public string QuotedSelectorName
        {
            get
            {
                if (string.IsNullOrEmpty(Alias)) return QuotedTableName;
                else return $"`{Alias}`";
            }
        }
        public string AliasDeclaring
        {
            get
            {
                if (string.IsNullOrEmpty(Alias) && string.IsNullOrEmpty(ParentSchema.Alias)) return QuotedTableName;
                else return $"{RealQuotedTableName} AS {QuotedSelectorName}";
            }
        }


        public override string ToString() => QuotedTableName;

        public string BuilderTarget => ToString();

        /// <summary>
        /// Switches from a user created schema-table to the corresponding RampTable
        /// </summary>
        /// <param name="table">Instance of the user table</param>
        /// <returns></returns>
        public static RampTableData SwitchBranch(IRampTable table) => ((RampColumn)table.GetType().GetProperties().Where(x => x.PropertyType == typeof(RampColumn)).First().GetValue(table)).ParentTable;
        public void AsAlias(string alias) => Alias = alias;
        public object[] GetParameterValues() => new object[0];
    }
}
