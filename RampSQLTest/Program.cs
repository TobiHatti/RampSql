using RampSQL.Schema;
using RampSQL.Search;
using System;

namespace RampSQLTest
{
    class Test
    {
        public string a = "123";
        public string b;
        public string c = null;
        public string d = String.Empty;

    }
    class Program
    {
        private static bool PropertyIsSet(Type t, object value)
        {
            if (value == null) return false;
            if (t.GetType().Equals(typeof(string)) && string.IsNullOrEmpty(value.ToString())) return false;
            object val = null;
            if (t.IsValueType) val = Activator.CreateInstance(t);
            else val = null;
            return !value.Equals(val);
        }

        static void Main(string[] args)
        {
            Test t = new Test();

            Console.WriteLine(PropertyIsSet(t.a.GetType(), t.a));
            Console.WriteLine(PropertyIsSet(t.a.GetType(), t.b));
            Console.WriteLine(PropertyIsSet(t.a.GetType(), t.c));
            Console.WriteLine(PropertyIsSet(t.a.GetType(), t.d));

            //CustomerModel c = new CustomerModel()
            //{
            //    ID = 1,
            //    Firstname = "Tobi",
            //    Lastname = "Hatti"
            //};

            //RampModelBinder binder = c.GetBinder();
            //QueryEngine query = new QueryEngine();


            //using (WrapMySQL sql = new WrapMySQL("", "", "", ""))
            //{
            //    sql.Open();
            //    using (RampReader reader = new RampReader(null))
            //    {
            //        int i = reader.GetInt32(MR.C.ID);
            //    }
            //    sql.Close();
            //}

            //Console.WriteLine(
            //    query
            //    .SelectFrom("Here")
            //    .All()
            //    .Columns(MR.C.ID, MR.C.L)
            //    .Column(MR.C.Birthday, "bday")
            //    .Value("text", "test")
            //    .InnerJoin(MR.C.ID, MR.C.ID)
            //    .LeftJoin(MR.C.L, MR.C.L)
            //    .Where
            //    .SectStart
            //    .Like(MR.C.L, "to", LikeWildcard.MatchAny)
            //    .And
            //    .NotLike(MR.C.L, "sa", LikeWildcard.MatchEnd)
            //    .SectEnd
            //    .Or
            //    .Is(MR.C.ID, 123)
            //    .GroupBy(MR.C.L)
            //    .OrderBy(MR.C.ID, SortDirection.Ascending)
            //    .Limit(12)
            //    .Shift(32)
            //);

            SearchEngine sqg = new SearchEngine(MR.C, "Hi", new RampColumn[]
                {
                    MR.C.L,
                    MR.C.Firstname
                },
                DuplicateResultRule.KeepLast)
                .AddSearchField(MR.C.Firstname, "Vorname")
                .AddSearchField(MR.C.L, "Nachname");

            Console.WriteLine(sqg.GetQuery());
        }
    }
}
