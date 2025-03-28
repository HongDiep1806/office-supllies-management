
using MediatR;

public class ApproveRequestFinEmployeeCommand : IRequest<bool>
{
    public int RequestId { get; set; }
    public string Note { get; set; } // Added property

    public ApproveRequestFinEmployeeCommand(int requestId, string note)
    {
        RequestId = requestId;
        Note = note;
    }
}