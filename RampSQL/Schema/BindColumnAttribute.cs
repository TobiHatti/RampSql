using System;

namespace RampSQL.Schema
{
    public class BindColumnAttribute : Attribute
    {
        public string ColumnName;
        public Type ColumnType;

        public BindColumnAttribute(string dbColumnName, Type dbColumnType)
        {
            ColumnName = dbColumnName;
            ColumnType = dbColumnType;
        }
    }
}
