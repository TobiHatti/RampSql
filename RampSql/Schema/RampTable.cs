namespace RampSql.Schema
{
    public class RampTable
    {
        public string DBTableName { get; }
        public Type Type { get; }
        public RampColumn[] Columns { get; set; }
        public RampSchema ParentSchema { get; }
        public RampTable(RampSchema schema, string dBTableName, Type type)
        {
            ParentSchema = schema;
            DBTableName = dBTableName;
            Type = type;
        }


        public override string ToString() => $"`{DBTableName}`";
    }
}
