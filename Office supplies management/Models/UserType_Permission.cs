namespace Office_supplies_management.Models
{
    public class UserType_Permission:BaseEntity
    {
        public int UserType_PermissionID { get; set; }
        public int UserTypeID { get; set; }
        public int PermissionID { get; set; }
        public UserType UserType { get; set; }
        public Permission Permission { get; set; }
    }
}
