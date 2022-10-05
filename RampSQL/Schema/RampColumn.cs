using System;

namespace RampSQL.Schema
{
    public class RampColumn : ICloneable
    {
        private RampTable table;
        private string columnName;
        private Type columnType;
        private string columnLabel;
        private bool columnIsPrimaryKey;
        private PrimaryKeyType columnPKType;
        public RampColumn(RampTable parentTable, string dbColumnName, Type dbColumnDataType, string label, bool isPrimaryKey, PrimaryKeyType primaryKeyType)
        {
            table = parentTable;
            columnName = dbColumnName;
            columnType = dbColumnDataType;
            columnLabel = label;
            columnIsPrimaryKey = isPrimaryKey;
            columnIsPrimaryKey = isPrimaryKey;
            columnPKType = primaryKeyType;
        }

        /// <summary>
        /// Return the fully qualified name ("`table`.`column`")
        /// </summary>
        public override string ToString() => FQN;

        public new Type GetType() => columnType;

        public object Clone()
        {
            return new RampColumn(table, columnName, columnType, columnLabel, columnIsPrimaryKey, columnPKType);
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
