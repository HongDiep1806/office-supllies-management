using System.ComponentModel.DataAnnotations.Schema;

namespace Office_supplies_management.DTOs.Summary
{
    public class CreateSummaryDto
    {
        public string SummaryCode { get; set; }
        public int UserID { get; set; }
        public List<int> RequestIDs { get; set; } 

    }
}
