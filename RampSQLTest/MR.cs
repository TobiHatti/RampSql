using RampSQL.Binder;
using RampSQL.Schema;
using System;

namespace RampSQLTest
{
    public class MR : IRampSchema
    {

        public static DBOrders Orders = new DBOrders();
        [BindTable("tab_customers")]
        public class DBOrders : RampTable
        {
            [BindColumn("ID", typeof(int))]
            public RampColumn ID { get; set; }
            [BindColumn("CustomerID", typeof(int))]
            public RampColumn CustomerID { get; set; }
        }

        public static DBCustomers Customers = new DBCustomers();
        [BindTable("tab_customers")]
        public class DBCustomers : RampTable
        {
            [BindColumn("ID", typeof(int))]
            public RampColumn ID { get; set; }
            [BindColumn("FName", typeof(string))]
            [ColumnLabel("Vorname")]
            public RampColumn Firstname { get; set; }
            [BindColumn("LName", typeof(string))]
            public RampColumn L { get; set; }
            [BindColumn("BDay", typeof(DateTime))]
            public RampColumn Birthday { get; set; }
            [BindColumn("stuff", typeof(int))]
            public RampColumn Stuff { get; set; }
        }
    }


    public class CustomerModel : IRampBindable
    {
        [BindProperty]
        public int ID { get; set; }
        [BindProperty]
        public string Firstname { get; set; }
        [BindProperty]
        public OrderModel[] Stuff { get; set; }
        [BindProperty]
        public string Lastname { get; set; }
        [BindProperty]
        public DateTime Birthdate { get; set; }

        public RampModelBinder GetBinder() => new RampModelBinder()
            .SetTarget(MR.Customers)
            .BindPrimaryKey(MR.Customers.ID, () => ID, (e) => ID = e)
            .Bind(MR.Customers.Firstname, () => Firstname, (e) => Firstname = e)
            .Bind(MR.Customers.L, () => Lastname, (e) => Lastname = e)
            .ReferenceBind(MR.Customers.Stuff, MR.Customers.L, () => Stuff, (e) => Stuff = e)
            .Bind(MR.Customers.Birthday, () => Birthdate, (e) => Birthdate = e);
    }

    public class OrderModel : IRampBindable
    {
        public int ID { get; set; }
        public CustomerModel[] Customers { get; set; }


        public RampModelBinder GetBinder() => new RampModelBinder()
            .SetTarget(MR.Customers)
            .BindPrimaryKey(MR.Orders.ID, () => ID, (e) => ID = e)
            .ReferenceBind(MR.Orders.CustomerID, MR.Customers.ID, () => Customers, (e) => Customers = e);
    }
}
