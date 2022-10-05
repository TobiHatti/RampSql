﻿using RampSQL.Binder;
using RampSQL.Query;
using RampSQL.Schema;
using System;

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


            //IQuerySection s = new QueryEngine()
            //    .SelectAllFrom(RDB.Houses, "mahHouse")
            //    .InnerJoin(RDB.Houses.As<RampHouses>("mahHouse").ID, RDB.Residents.HouseID)
            //    .Where.Is(RDB.Residents.Age, new QueryEngine().SelectFrom(RDB.Residents).Function(MySqlFunction.MAX, null, RDB.Residents.Age).Where.Is(RDB.Residents.Age, RDB.Houses.As<RampHouses>("mahHouse").ID));


            //IQuerySection a = s.Clone();

            //QueryEngine e = (QueryEngine)a;

            //e.Skip().Where.IsNotNull(RDB.Pets.PetName);

            Console.WriteLine(RDB.Houses.ID);

            ResidentModel res = new ResidentModel();
            RampModelBinder bin = res.GetBinder();

            var col = RampColumn.Columns;

            int[] test = new int[] { 0, 1, 2, 3, 4, 5 };


            foreach (var c in RampColumn.Columns)
            {
                Console.WriteLine(c.SUCN);
            }

            var ccc = RDB.Pets.As<RDB.RampPets>("pets");

            Console.WriteLine(
                new QueryEngine().SelectAllFrom(RDB.Pets, "pets")
                .LeftJoin(ccc.ResidentID, RDB.Residents.ID, "resis")
            );
        }
    }
}
