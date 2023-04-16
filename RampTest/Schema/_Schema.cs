using RampSql.Schema;

namespace RampTest.Schema
{
    public class Database : IRampSchema
    {
        [BindTable("Houses")]
        public TableHouses Houses { get; set; }
        [BindTable("Residents")]
        public TableResidents Residents { get; set; }
        [BindTable("Pets")]
        public TablePets Pets { get; set; }
        [BindTable("HouseTypes")]
        public TableHouseTypes HouseTypes { get; set; }

    }
}
