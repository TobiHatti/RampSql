using RampSQL.Query;
using RampSQL.Schema;
using RampSQL.Search;
using System;

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

            SearchEngine sqg = new SearchEngine(new QueryEngine().SearchFrom(MR.C).InnerJoin(MR.C.ID, MR.C.ID), "test", new RampColumn[]
            {
                MR.C.L,
                MR.C.Firstname,
                MR.C.ID
            })
                .AddSearchField(MR.C.L, "Bestellnummer")
                .AddSearchField(MR.C.Firstname, "Rechnungsnummer")
                .AddSearchField(MR.C.Birthday, "Name 1");

            Console.WriteLine(sqg.GetRampQuery().ToString());



            Console.WriteLine(new QueryEngine().InsertInto(MR.C).Value(MR.C.L, "hasde").Value(MR.C.Birthday, SQLFunction.NOW));
        }
    }
}
