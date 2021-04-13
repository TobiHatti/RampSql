using RampSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampSQLTest
{
    public class MyRamp : RampSchema
    {
        // Declare Target DB Type
        public MyRamp() : base(RampDBInterface.MySQL) { }

        // Declare Tables
        public RampTable_Customers Customers = new RampTable_Customers();
        public class RampTable_Customers : RampTable
        {
            // Specify the table-name
            public RampTable_Customers() : base(tableName: "customer") { }

            // Specify the columns and column names
            public RampColumn ID = new RampColumn("id", typeof(int));
            public RampColumn Name = new RampColumn("name", typeof(int));
            public RampColumn Street = new RampColumn("street", typeof(int));
            public RampColumn Country = new RampColumn("country", typeof(int));
        }

        public RampTable_Orders Orders = new RampTable_Orders();
        public class RampTable_Orders : RampTable
        {
            public RampTable_Orders() : base(tableName: "orders") { }

            public RampColumn ID = new RampColumn("id", typeof(int));
            public RampColumn Name = new RampColumn("name", typeof(int));
            public RampColumn Street = new RampColumn("street", typeof(int));
            public RampColumn Country = new RampColumn("country", typeof(int));
        }

        public RampTable_Invoices Invoices = new RampTable_Invoices();
        public class RampTable_Invoices : RampTable
        {
            public RampTable_Invoices() : base(tableName: "invoices") { }

            public RampColumn ID = new RampColumn("id", typeof(int));
            public RampColumn Name = new RampColumn("name", typeof(int));
            public RampColumn Street = new RampColumn("street", typeof(int));
            public RampColumn Country = new RampColumn("country", typeof(int));
        }

        public RampTable_Messages Messages = new RampTable_Messages();
        public class RampTable_Messages : RampTable
        {
            public RampTable_Messages() : base(tableName: "messages") { }

            public RampColumn ID = new RampColumn("id", typeof(int));
            public RampColumn Name = new RampColumn("name", typeof(int));
            public RampColumn Street = new RampColumn("street", typeof(int));
            public RampColumn Country = new RampColumn("country", typeof(int));
        }
    }
}
