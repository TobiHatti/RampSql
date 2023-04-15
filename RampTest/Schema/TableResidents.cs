using RampSql.Schema;

namespace RampTest.Schema
{
    public class TableResidents : IRampTable
    {
        [BindColumn("ID", typeof(int))]
        public RampColumn ID { get; set; }
        [BindColumn("HouseID", typeof(int))]
        public RampColumn HouseID { get; set; }
        [BindColumn("Name", typeof(string))]
        public RampColumn Name { get; set; }
        [BindColumn("Age", typeof(int))]
        public RampColumn Age { get; set; }
    }
}
