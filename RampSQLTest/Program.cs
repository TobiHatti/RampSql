using RampSQL.Query;
using RampSQL.Reader;
using RampSQL.Schema;
using RampSQL.Search;
using System;
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


            new RampReader(sql.ExecuteQuery("")).ReadAll((r) =>
            {
                s.Add(r.GetString(MR.Customers.L));
            });
        }
    }
}
