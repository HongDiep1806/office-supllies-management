namespace Office_supplies_management.Models
{
    public class Product : BaseEntity
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UnitCurrency { get; set; }
        public string UnitPrice { get; set; }
        public ICollection<Product_Request> Product_Requests { get; set; }
    }
}
