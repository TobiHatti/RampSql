namespace RampSql.Schema
{
    public class BindTableAttribute : Attribute
    {
        public readonly string TableName;
        public BindTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
