using System;

namespace RampSQL
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
