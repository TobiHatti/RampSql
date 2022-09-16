using RampSQL.Binder;
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
        }
    }
}
