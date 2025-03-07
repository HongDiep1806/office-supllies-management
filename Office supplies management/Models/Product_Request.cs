namespace Office_supplies_management.Models
{
    public class Product_Request:BaseEntity
    {
        public int Product_RequestID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public int RequestID { get; set; }
        public Product Product { get; set; }
        public Request Request { get; set; }

    }
}
