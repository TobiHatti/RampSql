using RampSQL.Binder;
using RampSQL.Schema;
using System;

namespace RampSQLTest
{
    public class MR : IRampSchema
    {
        public static DBCustomers C = new DBCustomers();

        [BindTable("tab_customers")]
        public class DBCustomers : RampTable
        {
            [BindColumn("ID")]
            public RampColumn ID { get; set; }
            [BindColumn("FName")]
            public RampColumn Firstname { get; set; }
            [BindColumn("LName")]
            public RampColumn L { get; set; }
            [BindColumn("BDay")]
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
            .SetTarget(MR.C)
            .BindPrimaryKey(MR.C.ID, () => ID, (e) => ID = e)
            .Bind(MR.C.Firstname, () => Firstname, (e) => Firstname = e)
            .Bind(MR.C.L, () => Lastname, (e) => Lastname = e)
            .Bind(MR.C.Birthday, () => Birthdate, (e) => Birthdate = e);

    }
}
