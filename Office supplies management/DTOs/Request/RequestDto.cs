using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.Models;

namespace Office_supplies_management.DTOs.Request
{
    public class RequestDto
    {
        public int RequestID { get; set; }
        public int TotalPrice { get; set; }
        public string RequestCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsProcessedByDepLead { get; set; }
        public bool IsApprovedByDepLead { get; set; }
        public bool IsApprovedBySupLead { get; set; }
        public int UserID { get; set; }
        public List<ProductRequestDto> Product_Requests { get; set; }

    }
}
