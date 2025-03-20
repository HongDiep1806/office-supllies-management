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
        public bool IsProcessedByDepLead { get; set; }
        public bool IsApprovedByDepLead { get; set; }
        public bool IsApprovedBySupLead { get; set; }
        public bool IsSummaryBeProcessed { get; set; }
        public bool IsSummaryBeApproved { get; set; }
        public bool IsCollectedInSummary { get; set; }
        public DateTime DateDepLeadApprove { get; set; }
        public string NoteDepLead { get; set; }
        public DateTime DateSupLeadApprove { get; set; }
        public string NoteSupLead { get; set; }

    }
}
