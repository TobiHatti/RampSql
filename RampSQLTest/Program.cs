using RampSQL.Query;
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

            Console.WriteLine(new QueryEngine().InsertInto(MR.C).Value(MR.C.L, "hasde").Value(MR.C.Birthday, SQLFunction.NOW));
        }
    }
}
