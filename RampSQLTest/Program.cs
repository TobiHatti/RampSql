using System;

namespace RampSQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyRamp ramp = new MyRamp();

            Console.WriteLine(ramp.Customers.Lastname);

        }
    }
}
