using RampSql.QueryBuilder;

namespace RampSql.Schema
{
    public class RampTable : IRampTable, IRampTarget
    {
        public string DBTableName { get; }
        public Type Type { get; }
        public RampColumn[] Columns { get; set; }
        public RampSchema ParentSchema { get; }
        private string alias = string.Empty;
        public string Alias
        {
            get => alias;
            set
            {
                if (!string.IsNullOrEmpty(value)) alias = value;
            }
        }
        public RampTable(RampSchema schema, string dBTableName, Type type)
        {
            ParentSchema = schema;
            DBTableName = dBTableName;
            Type = type;
        }


        public string QuotedTableName { get => $"`{DBTableName}`"; }
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
                if (string.IsNullOrEmpty(Alias)) return QuotedTableName;
                else return $"{QuotedTableName} AS {QuotedSelectorName}";
            }
        }


        public override string ToString() => QuotedTableName;

        public string BuilderTarget => ToString();

        /// <summary>
        /// Switches from a user created schema-table to the corresponding RampTable
        /// </summary>
        /// <param name="table">Instance of the user table</param>
        /// <returns></returns>
        public static RampTable SwitchBranch(IRampTable table) => ((RampColumn)table.GetType().GetProperties().Where(x => x.PropertyType == typeof(RampColumn)).First().GetValue(table)).ParentTable;
        public void AsAlias(string alias) => Alias = alias;
    }
}
