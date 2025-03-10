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
        public ICollection <Product_Request> Product_Requests { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }  

    }
} 
