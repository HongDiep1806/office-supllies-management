using Office_supplies_management.Models;

namespace Office_supplies_management.DTOs.User
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Department {  get; set; }
        //public bool IsProcessedByDepLead { get; set; } = false;
        //public bool IsApprovedByDepLead { get; set; } = false;
        //public bool IsApprovedBySupLead { get; set; } = false;
    }
}
