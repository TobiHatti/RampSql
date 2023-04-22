using RampSql.Schema;

namespace RampTest.Schema
{
    public class TableCalcOrderItems : IRampTable
    {
        [BindColumn("ID", typeof(int))]
        public RampColumn? ID { get; set; }
        [BindColumn("ParentHistoryID", typeof(int))]
        public RampColumn? ParentHistoryID { get; set; }
        [BindColumn("ItemID", typeof(int))]
        public RampColumn? ItemID { get; set; }
        [BindColumn("ItemCount", typeof(int))]
        public RampColumn? Count { get; set; }
        [BindColumn("ItemOriginalPrice", typeof(decimal))]
        public RampColumn? OriginalPrice { get; set; }
        [BindColumn("ItemEffectivePrice", typeof(decimal))]
        public RampColumn? EffectivePrice { get; set; }
        [BindColumn("ItemIsOptional", typeof(bool))]
        public RampColumn? IsOptional { get; set; }
        [BindColumn("ItemPosition", typeof(int))]
        public RampColumn? Position { get; set; }
        [BindColumn("ItemExtraParameters", typeof(string))]
        public RampColumn? ExtraParameters { get; set; }
    }
}
