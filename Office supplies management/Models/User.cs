namespace Office_supplies_management.Models
{
    public class User:BaseEntity
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public UserType UserType { get; set; }
        public ICollection<Request> Requests { get; set; }
        public string Department { get; set; } = string.Empty;
        public ICollection<Summary> Summaries { get; set; }
    }
}
