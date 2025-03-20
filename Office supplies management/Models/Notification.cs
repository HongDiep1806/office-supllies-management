using Office_supplies_management.Models;

public class Notification : BaseEntity
{
    public int NotificationID { get; set; }
    public int UserID { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
    public User User { get; set; }
    public int RequestID { get; set; }
    public int Sender { get; set; }
}