using System;
using System.Collections.Generic;

namespace RampSQL.Schema
{
    public class RampColumn : ICloneable
    {
        public static readonly List<RampColumn> Columns = new List<RampColumn>();

        private RampTable table;
        private string columnName;
        private Type columnType;
        private string columnLabel;
        private bool columnIsPrimaryKey;
        private PrimaryKeyType columnPKType;
        internal string columnAlias;
        internal bool requiresAlias;
        public RampColumn(RampTable parentTable, string dbColumnName, Type dbColumnDataType, string label, bool isPrimaryKey, PrimaryKeyType primaryKeyType)
        {
            table = parentTable;
            columnName = dbColumnName;
            columnType = dbColumnDataType;
            columnLabel = label;
            columnIsPrimaryKey = isPrimaryKey;
            columnIsPrimaryKey = isPrimaryKey;
            columnPKType = primaryKeyType;
            columnAlias = Guid.NewGuid().ToString().Replace("-", "");
            requiresAlias = false;

            Columns.Add(this);
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

        /// <summary>
        /// Return the save unquoted column name ("column")
        /// </summary>
        public string SUCN
        {
            get
            {
                if (requiresAlias) return columnAlias;
                else return columnName;
            }
        }

        public RampTable ParentTable { get => table; }

        public string Label { get => columnLabel; }
    }
}
