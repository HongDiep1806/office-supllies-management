public class ApproveRequestSupLeadCommand
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public ApproveRequestSupLeadCommand(int requestId, int userId, string userRole)
    {
        RequestId = requestId;
        UserId = userId;
        UserRole = userRole;
    }
}
