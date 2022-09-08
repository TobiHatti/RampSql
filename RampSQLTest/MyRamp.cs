using RampSQL;
using System;

namespace RampSQLTest
{
    public class MyRamp
    {
        public DBCustomers Customers = new DBCustomers();


        [BindTable("tab_customers")]
        public class DBCustomers : RampTable
        {
            [BindColumn("ID", typeof(int))]
            public RampColumn ID { get; set; }
            [BindColumn("FName", typeof(string))]
            public RampColumn Firstname { get; set; }
            [BindColumn("LName", typeof(string))]
            public RampColumn Lastname { get; set; }
            [BindColumn("BDay", typeof(DateTime))]
            public RampColumn Birthday { get; set; }
        }

    }
}
