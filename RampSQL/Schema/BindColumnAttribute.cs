namespace RampSql.Schema
{
    public class BindColumnAttribute : Attribute
    {
        public readonly string ColumnName;
        public readonly Type ColumnType;

        public BindColumnAttribute(string columnName, Type columnType)
        {
            ColumnName = columnName;
            ColumnType = columnType;
        }
    }
}
