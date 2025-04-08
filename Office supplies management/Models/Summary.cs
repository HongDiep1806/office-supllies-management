using System.ComponentModel.DataAnnotations.Schema;

namespace Office_supplies_management.Models
{
    public class Summary : BaseEntity
    {
        public int SummaryID { get; set; }
        public int UserID { get; set; }
        public string SummaryCode { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiredTime { get; set; } = DateTime.Now.AddMinutes(2);
        [InverseProperty("Summary")]
        public ICollection<Request> Requests { get; set; } = new List<Request>();
        public int TotalPrice { get; set; }
        public bool IsProcessedBySupLead { get; set; } = false;
        public bool IsApprovedBySupLead { get; set; } = false ;
        public DateTime? UpdateDate { get; set; } // Add this property
    }
}
