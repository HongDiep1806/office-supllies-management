public class CreateProductDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string UnitCurrency { get; set; }
    public string UnitPrice { get; set; }
    public int UserIDCreate { get; set; }
    public int UserIDAdjust { get; set; }
}