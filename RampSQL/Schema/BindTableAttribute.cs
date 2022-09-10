using System;

namespace RampSQL.Schema
{
    public class BindTableAttribute : Attribute
    {
        public string TableName;
        public BindTableAttribute(string dbTableName)
        {
            TableName = dbTableName;
        }
    }
}
