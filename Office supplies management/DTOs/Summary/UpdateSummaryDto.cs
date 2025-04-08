using Office_supplies_management.DTOs.ProductRequest;

namespace Office_supplies_management.DTOs.Request
{
    public class UpdateSummaryDto
    {
        public int SummaryID { get; set; }
        public bool IsProcessedBySupLead { get; set; }
        public bool IsApprovedBySupLead { get; set; }
        public DateTime? UpdateDate { get; set; } // Add this property

    }
}
