using Office_supplies_management.Models;

public class Product : BaseEntity
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string UnitCurrency { get; set; }
    public string UnitPrice { get; set; }
    public int UserIDCreate { get; set; }
    public DateTime CreateDate { get; set; }
    public int UserIDAdjust { get; set; }
    public DateTime AdjustDate { get; set; }
    public ICollection<Product_Request> Product_Requests { get; set; }
}