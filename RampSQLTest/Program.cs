using RampSQL.Binder;
using RampSQL.Query;
using System;
using static RampSQLTest.RDB;

namespace RampSQLTest
{

    class Program
    {
        static void Main(string[] args)
        {
            PetsModel pet = new PetsModel()
            {
                PetName = "Yuki",
                AnimalType = Animals.Cat,
                ResidentID = 1,
                ID = 2
            };

            RampModelBinder binder = pet.GetBinder();

            binder.Binds[2].Set(Animals.Dog);
            binder.Binds[2].Set("Fish");
            binder.Binds[1].Set("Adi");
            Console.WriteLine(binder.Binds[2].Get());
            Console.WriteLine(binder.Binds[1].Get());


            Console.WriteLine(new QueryEngine()
                .SelectAllFrom(RDB.Houses, "mahHouse")
                .InnerJoin(RDB.Houses.As<RampHouses>("mahHouse").ID, RDB.Residents.HouseID)
                .Where.Is(RDB.Residents.Age, new QueryEngine().SelectFrom(RDB.Residents).Function(MySqlFunction.MAX, null, RDB.Residents.Age).Where.Is(RDB.Residents.Age, RDB.Houses.As<RampHouses>("mahHouse").ID))

                );
            Console.WriteLine(RDB.Houses.ID);

            ResidentModel res = new ResidentModel();
            RampModelBinder bin = res.GetBinder();
        }
    }
}
