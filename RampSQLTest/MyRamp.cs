using RampSQL.Binder;
using RampSQL.Schema;
using System;

namespace RampSQLTest
{
    public class MyRamp : IRampSchema
    {
        public static DBCustomers Customers = new DBCustomers();

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


    public class CustomerModel
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }

        public RampModelBinder GetBinder() => new RampModelBinder()
            .SetTarget(MyRamp.Customers)
            .BindPrimaryKey(MyRamp.Customers.ID, () => ID, (e) => ID = e)
            .Bind(MyRamp.Customers.Firstname, () => Firstname, (e) => Firstname = e)
            .Bind(MyRamp.Customers.Lastname, () => Lastname, (e) => Lastname = e)
            .Bind(MyRamp.Customers.Birthday, () => Birthdate, (e) => Birthdate = e);

    }
}
