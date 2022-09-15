using RampSQL.Binder;
using RampSQL.Query;
using RampSQL.Schema;
using RampSQL.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using WrapSQL;

namespace RampSQLTest
{

    class Program
    {
        public enum Test
        {
            a,
            b,
            c,
            d,
            e,


        }
        static void Main(string[] args)
        {
            //RampReader reader = new RampReader(null);

            //reader.GetEnum<Test>(null);


            UInt64 a = 123;
            int d = (int)a;

            SearchEngine sqg = new SearchEngine(new QueryEngine().SearchFrom(MR.Customers).InnerJoin(MR.Customers.ID, MR.Customers.ID), "test", new RampColumn[]
            {
                MR.Customers.L,
                MR.Customers.Firstname,
                MR.Customers.ID
            })
                .AddSearchField(MR.Customers.L, "Bestellnummer")
                .AddSearchField(MR.Customers.Firstname, "Rechnungsnummer")
                .AddSearchField(MR.Customers.Birthday, "Name 1");

            Console.WriteLine(sqg.GetRampQuery().ToString());



            Console.WriteLine(new QueryEngine().InsertInto(MR.Customers).Value(MR.Customers.L, "hasde").Value(MR.Customers.Birthday, SQLFunction.NOW));

            WrapMySQL sql = new WrapMySQL("", "", "", "");

            List<string> s = new List<string>();


            CustomerModel customerModel = new CustomerModel();
            RampModelBinder binder = customerModel.GetBinder();

            foreach (RampModelBinder.BindEntry entry in binder.Binds)
            {
                if (entry.BindType == RampModelBinder.BindType.ReferenceArray)
                {
                    IRampBindable[] orders = new OrderModel[10];

                    for (int i = 0; i < orders.Length; i++)
                    {
                        orders[i] = (IRampBindable)Activator.CreateInstance(entry.Type);
                    }

                    List<IRampBindable> list = new List<IRampBindable>();
                    list.Add((IRampBindable)Activator.CreateInstance(entry.Type));

                    var adff = list.ToArray();

                    var ass = entry.Type;

                    Type genericListType = typeof(List<>).MakeGenericType(entry.Type);
                    IList listnew = (IList)Activator.CreateInstance(genericListType);
                    listnew.Add((IRampBindable)Activator.CreateInstance(entry.Type));

                    IRampBindable[] resArray = (IRampBindable[])Activator.CreateInstance(entry.Type.MakeArrayType(), listnew.Count);
                    listnew.CopyTo(resArray, 0);

                    entry.Set(adff);
                }
            }


        }
    }
}
