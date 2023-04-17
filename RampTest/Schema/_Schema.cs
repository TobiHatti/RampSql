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


        [BindTable("calc_orders")]
        public TableCalcOrders Orders { get; set; }
        [BindTable("calc_order_history")]
        public TableCalcOrderHistory History { get; set; }
        [BindTable("calc_order_items")]
        public TableCalcOrderItems Items { get; set; }
    }
}
