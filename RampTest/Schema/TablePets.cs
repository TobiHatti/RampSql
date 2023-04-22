using RampSql.Schema;

namespace RampTest.Schema
{
    public class TablePets : IRampTable
    {
        [BindColumn("ID", typeof(int))]
        public RampColumn ID { get; set; }
        [BindColumn("ResidentID", typeof(int))]
        public RampColumn ResidentID { get; set; }
        [BindColumn("PetName", typeof(string))]
        public RampColumn PetName { get; set; }
        [BindColumn("AnimalType", typeof(string))]
        public RampColumn AnimalType { get; set; }
    }
}
