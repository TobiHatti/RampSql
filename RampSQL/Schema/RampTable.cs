using System;
using System.Reflection;

namespace RampSQL.Schema
{
    public class RampTable : IRampTable
    {
        public string TableName { get; set; }

        public RampTable()
        {
            InitializeRampTable();
            InitializeRampColumns();
            CheckColumnAliases();
        }

        public RampTable(string tableAlias) : this()
        {
            TableName = tableAlias;
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

        public T As<T>(string alias) where T : IRampTable
        {
            T aliasTable = (T)Activator.CreateInstance(typeof(T));
            aliasTable.TableName = alias;
            return aliasTable;
        }

        public override string ToString() => $"`{TableName}`";
    }
}
