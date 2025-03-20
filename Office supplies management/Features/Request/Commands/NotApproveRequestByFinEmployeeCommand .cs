using MediatR;

public class NotApproveRequestByFinEmployeeCommand : IRequest<bool>
{
    public int RequestID { get; set; }
    public string Note { get; set; }

    public NotApproveRequestByFinEmployeeCommand(int requestID, string note)
    {
        RequestID = requestID;
        Note = note;
    }
}
