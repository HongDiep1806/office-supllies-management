namespace Office_supplies_management.DTOs.Product
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UnitCurrency { get; set; }
        public string UnitPrice { get; set; }
        public bool IsDeleted { get; set; }
        public int UserIDCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserIDAdjust { get; set; }
        public DateTime AdjustDate { get; set; }
    }
}
