using System.ComponentModel.DataAnnotations.Schema;

namespace Office_supplies_management.Models
{
    public class Request : BaseEntity
    {
        public int RequestID { get; set; }
        public int TotalPrice { get; set; }
        public string RequestCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsProcessedByDepLead { get; set; }
        public bool IsApprovedByDepLead { get; set; }
        public bool IsApprovedBySupLead { get; set; }
        public bool IsCollectedInSummary { get; set; } = false;
        public bool IsSummaryBeProcessed { get; set; } = false;
        public bool IsSummaryBeApproved { get; set; } = false;
        public ICollection <Product_Request> Product_Requests { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int? SummaryID { get; set; }
        [ForeignKey("SummaryID")]
        public virtual Summary? Summary { get; set; }
        public DateTime DateDepLeadApprove { get; set; }
        public string NoteDepLead { get; set; }
        public DateTime DateSupLeadApprove { get; set; }
        public string NoteSupLead { get; set; }
    }
} 
