using System.Reflection;

namespace RampSql.Schema
{
    public class RampSchema
    {
        public Type Type { get; set; }
        public RampTable[] Tables { get; set; }
        public IRampSchema Instance { get; }

        public RampSchema(Type schemaType)
        {
            Type = schemaType;
            Instance = (IRampSchema)Activator.CreateInstance(Type);
            RegisterTables();
        }

        private void RegisterTables()
        {
            Dictionary<string, int> nameIndexer = new Dictionary<string, int>();

            List<RampTable> tables = new List<RampTable>();
            foreach (PropertyInfo table in Type.GetProperties())
            {
                if (table.PropertyType.GetInterfaces().Contains(typeof(IRampTable)))
                {
                    string tableName = string.Empty;
                    foreach (object attr in table.GetCustomAttributes(true))
                    {
                        BindTableAttribute btAttr = attr as BindTableAttribute;
                        if (btAttr is not null)
                        {
                            tableName = btAttr.TableName;
                        }
                    }
                    if (string.IsNullOrEmpty(tableName)) throw new RampException($"No bind attribute for '{table.Name}' provided!");

                    RampTable tab = new RampTable(this, tableName, table.PropertyType);
                    IRampTable tabInstance = (IRampTable)Activator.CreateInstance(table.PropertyType);
                    table.SetValue(Instance, tabInstance);
                    tables.Add(tab);

                    RegisterColumns(tabInstance, tab, nameIndexer);
                }
            }
            Tables = tables.ToArray();
        }

        private void RegisterColumns(IRampTable tabInstance, RampTable parentTable, Dictionary<string, int> nameIndexer)
        {
            List<RampColumn> columns = new List<RampColumn>();
            foreach (PropertyInfo column in tabInstance.GetType().GetProperties())
            {
                if (column.PropertyType == typeof(RampColumn))
                {
                    string columnName = string.Empty;
                    Type columnType = null;
                    foreach (object attr in column.GetCustomAttributes(true))
                    {
                        BindColumnAttribute bcAttr = attr as BindColumnAttribute;
                        if (bcAttr is not null)
                        {
                            columnName = bcAttr.ColumnName;
                            columnType = bcAttr.ColumnType;
                        }
                    }
                    if (string.IsNullOrEmpty(columnName)) throw new RampException($"No bind attribute for '{column.Name}' provided!");


                    string alias = string.Empty;
                    if (nameIndexer.ContainsKey(columnName)) alias = $"{columnName}_{nameIndexer[columnName]++}";
                    else nameIndexer.Add(columnName, 1);

                    RampColumn col = new RampColumn(parentTable, columnName, columnType, alias);
                    column.SetValue(tabInstance, col);
                    columns.Add(col);
                }
            }

            parentTable.Columns = columns.ToArray();
        }
    }
}
