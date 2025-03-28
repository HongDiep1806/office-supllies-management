using MediatR;

public class ApproveRequestByDepLeaderCommand : IRequest<bool>
{
    public int RequestID { get; set; }
    public string Note { get; set; } // Added property

    public ApproveRequestByDepLeaderCommand(int requestID, string note)
    {
        RequestID = requestID;
        Note = note;
    }
}