using System.ComponentModel.DataAnnotations;

namespace Office_supplies_management.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string UnitCurrency { get; set; }
        public string UnitPrice { get; set; }
    }
}
