using System.Reflection;

namespace RampSql.Schema
{
    public class RampSchemaData : ICloneable
    {
        public Type Type { get; set; }
        public RampTableData[] Tables { get; set; }
        public IRampSchema Instance { get; private set; }
        internal bool IsBackCall { get; set; } = false;
        internal string Alias { get; set; }
        internal string SubQueryPrefix
        {
            get
            {
                if (string.IsNullOrEmpty(Alias)) return string.Empty;
                else return $"{Alias}_";
            }
        }
        public RampSchemaData(Type schemaType)
        {
            Type = schemaType;
            Instance = (IRampSchema)Activator.CreateInstance(Type);
            RegisterTables();
        }

        public void SetInstance(IRampSchema schema) => Instance = schema;

        private void RegisterTables()
        {
            Dictionary<string, int> nameIndexer = new();

            List<RampTableData> tables = new();
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

                    RampTableData tab = new(this, tableName, table.PropertyType);
                    IRampTable tabInstance = (IRampTable)Activator.CreateInstance(table.PropertyType);
                    table.SetValue(Instance, tabInstance);
                    tables.Add(tab);

                    RegisterColumns(tabInstance, tab, nameIndexer);
                }
            }
            Tables = tables.ToArray();
        }

        private void RegisterColumns(IRampTable tabInstance, RampTableData parentTable, Dictionary<string, int> nameIndexer)
        {
            List<RampColumn> columns = new();
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

                    RampColumn col = new(parentTable, columnName, columnType, alias);
                    column.SetValue(tabInstance, col);
                    columns.Add(col);
                }
            }

            parentTable.Columns = columns.ToArray();
        }

        public static RampSchemaData SwitchBranch(IRampSchema schema)
        {
            IRampTable table = (IRampTable)schema.GetType().GetProperties().Where(x => x.PropertyType.GetInterfaces().Contains(typeof(IRampTable))).First().GetValue(schema);
            RampTableData rampTable = ((RampColumn)table.GetType().GetProperties().Where(x => x.PropertyType == typeof(RampColumn)).First().GetValue(table)).ParentTable;
            return rampTable.ParentSchema;
        }

        public static Schema CreateSub<Schema>(Schema sourceDBSchema) where Schema : RampSchema<Schema>
        {
            RampSchemaData newSchema = (RampSchemaData)SwitchBranch(sourceDBSchema).Clone();
            return (Schema)newSchema.Instance;
        }

        public object Clone()
        {
            RampSchemaData schema = new(Type)
            {
                Alias = Alias,
                IsBackCall = IsBackCall
            };
            return schema;
        }
    }
}
