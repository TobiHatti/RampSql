using RampSql.Schema;

namespace RampTest.Schema
{
    public class Database : IRampSchema
    {
        [BindTable("Customers")]
        public TableHouses Houses { get; set; }
        public TableResidents Residents { get; set; }
        public TablePets Pets { get; set; }
        public TableHouseTypes HouseTypes { get; set; }

    }
}
