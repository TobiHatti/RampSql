namespace RampSql.Schema
{
    public class RampColumn : IRampColumn
    {
        public string DBColumnName { get; }
        public Type Type { get; }
        public RampTable ParentTable { get; }

        public RampColumn(RampTable parentTable, string dBColumnName, Type type)
        {
            DBColumnName = dBColumnName;
            Type = type;
            ParentTable = parentTable;
        }

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public string FQN { get => $"{ParentTable}.`{DBColumnName}`"; }

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
    }
}
