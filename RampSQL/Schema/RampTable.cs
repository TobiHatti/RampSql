using RampSQL.Query;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RampSQL.Schema
{
    public class RampTable : IRampTable, IRampSelectable
    {
        internal static List<RampTable> Tables = new List<RampTable>();
        public string TableName { get; set; }
        public string TableAlias { get; set; }
        public string CustomAlias { get; set; } = null;
        public bool UseAlias { get; set; } = false;

        public string GetTableName
        {
            get
            {
                if (UseAlias)
                {
                    if (string.IsNullOrEmpty(CustomAlias)) return TableAlias;
                    else return CustomAlias;
                }
                else return TableName;
            }
        }

        public string AliasDeclaration
        {
            get
            {
                if (UseAlias)
                {
                    if (string.IsNullOrEmpty(CustomAlias)) return $"`{TableName}` AS `{TableAlias}`";
                    else return $"`{TableName}` AS `{CustomAlias}`";
                }
                else return TableName;
            }
        }
        public string RealName { get => $"`{TableName}`"; }
        public string AliasName
        {
            get
            {
                if (string.IsNullOrEmpty(CustomAlias)) return $"`{TableAlias}`";
                else return $"`{CustomAlias}`";
            }
        }
        public void SetAlias(string alias) => CustomAlias = alias;

        public RampTable()
        {
            InitializeRampTable();
            InitializeRampColumns();
            CheckColumnAliases();
            TableAlias = Guid.NewGuid().ToString().Replace("-", "");
            Tables.Add(this);
        }

        public RampTable(string tableName, string tableAlias) : this()
        {
            TableName = tableName;
            CustomAlias = tableAlias;
        }

        internal static void ResetAliases()
        {
            foreach (RampTable table in Tables)
            {
                table.UseAlias = false;
                table.CustomAlias = null;
            }
        }

        private void InitializeRampTable()
        {
            BindTableAttribute btAttr = (BindTableAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(BindTableAttribute));
            if (btAttr != null)
            {
                TableName = btAttr.TableName;
            }
        }

        private void InitializeRampColumns()
        {
            PropertyInfo[] pinfos = this.GetType().GetProperties();
            foreach (PropertyInfo pi in pinfos)
            {
                if (pi.PropertyType == typeof(RampColumn))
                {
                    string columnName = string.Empty;
                    Type columnType = typeof(object);
                    string columnLabel = string.Empty;
                    bool isPK = false;
                    PrimaryKeyType pkType = PrimaryKeyType.AutoIncrement;

                    object[] attributes = pi.GetCustomAttributes(true);
                    foreach (object attr in attributes)
                    {
                        BindColumnAttribute bcAttr = attr as BindColumnAttribute;
                        if (bcAttr != null)
                        {
                            columnName = bcAttr.ColumnName;
                            columnType = bcAttr.ColumnType;
                        }

                        ColumnLabelAttribute clAttr = attr as ColumnLabelAttribute;
                        if (clAttr != null)
                        {
                            columnLabel = clAttr.Label;
                        }

                        RampPrimaryKeyAttribute pkAttr = attr as RampPrimaryKeyAttribute;
                        if (pkAttr != null)
                        {
                            isPK = true;
                            pkType = pkAttr.PrimaryKeyType;
                        }
                    }

                    RampColumn col = (RampColumn)Activator.CreateInstance(pi.PropertyType, this, columnName, columnType, columnLabel, isPK, pkType);
                    pi.SetValue(this, col);
                }
            }
        }

        private void CheckColumnAliases()
        {
            bool cont = false;
            foreach (RampColumn column in RampColumn.Columns)
            {
                cont = false;
                foreach (RampColumn c in RampColumn.Columns)
                {
                    if (cont) continue;
                    if (column.columnAlias != c.columnAlias && column.UCN == c.UCN)
                    {
                        column.requiresAlias = true;
                        cont = true;
                    }
                }
            }
        }

        public T As<T>(string alias) where T : RampTable
        {
            CustomAlias = alias;
            UseAlias = true;
            return (T)this;
        }

        public RampTable As(string alias)
        {
            CustomAlias = alias;
            UseAlias = true;
            return this;
        }


        public override string ToString() => $"`{TableName}`";


    }
}
