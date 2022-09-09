using RampSQL;
using System;

namespace RampSQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyRamp ramp = new MyRamp();




            CustomerModel c = new CustomerModel()
            {
                ID = 1,
                Firstname = "Tobi",
                Lastname = "Hatti"
            };


            RampModelBinder binder = c.GetBinder();


            Console.WriteLine(binder.PrimaryKey.GetProperty());
            Console.WriteLine(binder.Binds[0].GetProperty());
            Console.WriteLine(binder.Binds[1].GetProperty());

            binder.Binds[0].SetProperty("Max");
            binder.Binds[1].SetProperty("Mustermann");

            Console.WriteLine(c.ID);
            Console.WriteLine(c.Firstname);
            Console.WriteLine(c.Lastname);

            Console.WriteLine(MyRamp.Customers.Lastname);


            //c.SaveModel();
            //c.LoadModel(1);

            RampQueryEngine engine = new RampQueryEngine();


            Console.WriteLine(engine.DeleteFrom(MyRamp.Customers).Where.Is(MyRamp.Customers.Lastname, "Tobi"));
            Console.WriteLine(engine.Update(MyRamp.Customers).ValuePair(MyRamp.Customers.Lastname, "Tobi").ValuePair(MyRamp.Customers.Lastname, "Hatti").Where.Is(MyRamp.Customers.ID, 123).And.Is(MyRamp.Customers.Lastname, "maasd"));
            Console.WriteLine(engine.InsertInto(MyRamp.Customers).ValuePair(MyRamp.Customers.Lastname, "Tobi").ValuePair(MyRamp.Customers.Lastname, "Hatti"));
            //Console.WriteLine(engine.SelectFrom(MyRamp.Customers).All().Columns(MyRamp.Customers.ID, MyRamp.Customers.Lastname).Alias(MyRamp.Customers.Birthday, "ISSDA").InnerJoin(MyRamp.Customers.Lastname, MyRamp.Customers.Birthday).Where.Like(MyRamp.Customers.Lastname, "Tob", LikeWildcard.MatchAny).Limit(10).SortBy(MyRamp.Customers.Lastname, SortDirection.Descending));
            Console.WriteLine(engine.SelectFrom(MyRamp.Customers).All().Order.By(MyRamp.Customers.Lastname, SortDirection.Descending).Result.Limit(10));
        }
    }
}
