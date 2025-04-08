namespace Office_supplies_management.DTOs.Summary
{
    public class SummaryDto
    {
        public int SummaryID { get; set; }
        public int UserID { get; set; }
        public string SummaryCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int TotalPrice { get; set; }
        public bool IsProcessedBySupLead { get; set; }
        public bool IsApprovedBySupLead { get; set; }
        public List<int> RequestIDs { get; set; }
        public DateTime? UpdateDate { get; set; } // Add this property
    }
}
