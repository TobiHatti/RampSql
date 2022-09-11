using RampSQL.Binder;
using RampSQL.Query;
using System;

namespace RampSQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MR ramp = new MR();

            CustomerModel c = new CustomerModel()
            {
                ID = 1,
                Firstname = "Tobi",
                Lastname = "Hatti"
            };

            RampModelBinder binder = c.GetBinder();
            QueryEngine query = new QueryEngine();



            Console.WriteLine(
                query
                .SelectFrom("Here")
                .All()
                .Columns(MR.C.ID, MR.C.L)
                .Column(MR.C.Birthday, "bday")
                .Value("text", "test")
                .InnerJoin(MR.C.ID, MR.C.ID)
                .LeftJoin(MR.C.L, MR.C.L)
                .Where
                .SectStart
                .Like(MR.C.L, "to", LikeWildcard.MatchAny)
                .And
                .NotLike(MR.C.L, "sa", LikeWildcard.MatchEnd)
                .SectEnd
                .Or
                .Is(MR.C.ID, 123)
                .GroupBy(MR.C.L)
                .OrderBy(MR.C.ID, SortDirection.Ascending)
                .Limit(12)
                .Shift(32)
            );
        }
    }
}
