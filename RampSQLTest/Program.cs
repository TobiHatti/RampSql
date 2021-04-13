using RampSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampSQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyRamp schema = new MyRamp();

            schema.Build.Select(schema.Customers.All).IsEqual(schema.Customers.Name, "test").Order(schema.Customers.ID);
        }
    }
}
