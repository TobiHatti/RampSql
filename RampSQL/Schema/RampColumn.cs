using System;

namespace RampSQL.Schema
{
    public class RampColumn : ICloneable
    {
        private RampTable table;
        private string columnName;
        private Type columnType;
        private string columnLabel;
        public RampColumn(RampTable parentTable, string dbColumnName, Type dbColumnDataType, string label)
        {
            table = parentTable;
            columnName = dbColumnName;
            columnType = dbColumnDataType;
            columnLabel = label;
        }

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public override string ToString() => FQN;

        public new Type GetType() => columnType;

        public object Clone()
        {
            return new RampColumn(table, columnName, columnType, columnLabel);
        }

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

        public RampTable ParentTable { get => table; }

        public string Label { get => columnLabel; }
    }
}
