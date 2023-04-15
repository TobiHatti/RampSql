using RampSql.Schema;

namespace RampTest.Schema
{
    public class TableHouseTypes : IRampTable
    {
        [BindColumn("ID", typeof(string))]
        public RampColumn ID { get; set; }
        [BindColumn("DisplayText", typeof(string))]
        public RampColumn DisplayText { get; set; }
    }
}
