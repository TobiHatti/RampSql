using System;

namespace RampSQL
{
    public class RampColumn
    {
        private RampTable table;
        private string columnName;
        private Type columnType;
        public RampColumn(RampTable parentTable, string dbColumnName, Type dbColumnDataType)
        {
            table = parentTable;
            columnName = dbColumnName;
            columnType = dbColumnDataType;
        }

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public override string ToString() => FQN;

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public string FQN { get => $"{table}.`{columnName}`"; }

        /// <summary>
        /// Return the quoted column name ("`column`")
        /// </summary>
        public string QCN { get => $"`{columnName}`"; }

        /// <summary>
        /// Return the unquoted column name ("column")
        /// </summary>
        public string UCN { get => columnName; }
    }
}
