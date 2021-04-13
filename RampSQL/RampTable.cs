using System;
using System.Dynamic;

namespace RampSQL
{
    public class RampTable
    {
        public RampColumn All = new RampColumn("*");

        public RampTable(string tableName)
        {

        }

        public RampColumn this[string columnName]
        {
            get
            {
                return null;
            }
        }
    }
}
