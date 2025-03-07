namespace Office_supplies_management.Models
{
    public class Permission:BaseEntity
    {
        public int PermissionID { get; set; }
        public string Description { get; set; }
        public ICollection<UserType> UserTypes { get; set; }    
    }
}
