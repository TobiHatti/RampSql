using RampSql.Schema;

namespace RampTest.Schema
{
    public class TableCalcOrders : IRampTable
    {
        [BindColumn("ID", typeof(int))]
        public RampColumn? ID { get; set; }
        [BindColumn("OrderNumber", typeof(string))]
        public RampColumn? OrderNumber { get; set; }
        [BindColumn("CustomerID", typeof(int))]
        public RampColumn? CustomerID { get; set; }
    }
}
