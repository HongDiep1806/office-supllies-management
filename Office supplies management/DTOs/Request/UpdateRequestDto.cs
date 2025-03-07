using Office_supplies_management.DTOs.ProductRequest;

namespace Office_supplies_management.DTOs.Request
{
    public class UpdateRequestDto
    {
        public int RequestID { get; set; }
        public int TotalPrice { get; set; }
        public string RequestCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        public List<ProductRequestDto> Products { get; set; }

    }
}
