using RampSql.Schema;

namespace RampTest.Schema
{
    public class TableCalcOrderHistory : IRampTable
    {
        [BindColumn("ID", typeof(int))]
        public RampColumn? ID { get; set; }
        [BindColumn("KatoVersion", typeof(string))]
        public RampColumn? Version { get; set; }
        [BindColumn("ParentOrderID", typeof(int))]
        public RampColumn? ParentOrderID { get; set; }
        [BindColumn("Permalink", typeof(string))]
        public RampColumn? Permalink { get; set; }
        [BindColumn("OrderIteration", typeof(int))]
        public RampColumn? OrderIteration { get; set; }
        [BindColumn("DisplayOrderNumber", typeof(string))]
        public RampColumn? DisplayOrderNumber { get; set; }
        [BindColumn("CreatedDate", typeof(DateTime))]
        public RampColumn? CreatedDate { get; set; }
        [BindColumn("UserID", typeof(int))]
        public RampColumn? UserID { get; set; }
        [BindColumn("SurchargePercentage", typeof(decimal))]
        public RampColumn? SurchargePercentage { get; set; }
        [BindColumn("DiscountPercentage", typeof(decimal))]
        public RampColumn? DiscountPercentage { get; set; }
        [BindColumn("DiscountAbsolute", typeof(decimal))]
        public RampColumn? DiscountAbsolute { get; set; }
        [BindColumn("VATPercentage", typeof(decimal))]
        public RampColumn? VATPercentage { get; set; }
        [BindColumn("SkontoPercentage", typeof(decimal))]
        public RampColumn? SkontoPercentage { get; set; }
        [BindColumn("Flatrate", typeof(decimal))]
        public RampColumn? Flatrate { get; set; }
        [BindColumn("CoverImage", typeof(string))]
        public RampColumn? CoverImage { get; set; }
        [BindColumn("CoverImageID", typeof(int))]
        public RampColumn? CoverImageID { get; set; }
        [BindColumn("NetSum", typeof(decimal))]
        public RampColumn? NetSum { get; set; }
        [BindColumn("NetSumPlusSkonto", typeof(decimal))]
        public RampColumn? NetSumPlusSkonto { get; set; }
        [BindColumn("GrossSum", typeof(decimal))]
        public RampColumn? GrossSum { get; set; }
        [BindColumn("GrossSumPlusSkonto", typeof(decimal))]
        public RampColumn? GrossSumPlusSkonto { get; set; }
        [BindColumn("PriceStatus", typeof(decimal))]
        public RampColumn? PriceStatus { get; set; }
        [BindColumn("VAT", typeof(decimal))]
        public RampColumn? VAT { get; set; }
        [BindColumn("OCReceiptNumber", typeof(string))]
        public RampColumn? OCReceiptNumber { get; set; }
        [BindColumn("OCCustomerNumber", typeof(string))]
        public RampColumn? OCCustomerNumber { get; set; }
        [BindColumn("OCDate", typeof(DateTime))]
        public RampColumn? OCDate { get; set; }
        [BindColumn("DocUserID", typeof(int))]
        public RampColumn? DocUserID { get; set; }
        [BindColumn("BVHLabel", typeof(string))]
        public RampColumn? BVHLabel { get; set; }
    }
}
