namespace Office_supplies_management.Models
{
    public class UserType:BaseEntity
    {
        public int UserTypeID { get; set; }
        public string Type { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
