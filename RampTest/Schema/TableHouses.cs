using RampSql.Schema;

namespace RampTest.Schema
{
    public class TableHouses : IRampTable
    {
        [BindColumn("ID", typeof(int))]
        public RampColumn ID { get; set; }
        [BindColumn("Housename", typeof(string))]
        public RampColumn HouseName { get; set; }
        [BindColumn("HouseNumber", typeof(int))]
        public RampColumn HouseNumber { get; set; }
        [BindColumn("IsBungalow", typeof(bool))]
        public RampColumn IsBungalow { get; set; }
        [BindColumn("HouseType", typeof(bool))]
        public RampColumn HouseType { get; set; }

    }
}
