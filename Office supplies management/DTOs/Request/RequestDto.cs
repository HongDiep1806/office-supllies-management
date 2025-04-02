using Office_supplies_management.DTOs.ProductRequest;

public class RequestDto
{
    public int RequestID { get; set; }
    public int TotalPrice { get; set; }
    public string RequestCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsProcessedByDepLead { get; set; }
    public bool IsApprovedByDepLead { get; set; }
    public bool IsApprovedBySupLead { get; set; }
    public bool IsSummaryBeProcessed { get; set; }
    public bool IsSummaryBeApproved { get; set; }
    public bool IsCollectedInSummary { get; set; }
    public int UserID { get; set; }
    public int? SummaryID { get; set; }
    public List<ProductRequestDto> Product_Requests { get; set; } // Ensure this is included
    public DateTime DateDepLeadApprove { get; set; }
    public string NoteDepLead { get; set; }
    public DateTime DateSupLeadApprove { get; set; }
    public string NoteSupLead { get; set; }
    public bool IsDeleted { get; set; } // Add this property
}
