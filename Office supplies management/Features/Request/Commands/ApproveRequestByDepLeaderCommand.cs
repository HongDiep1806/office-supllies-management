using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class ApproveRequestByDepLeaderCommand: IRequest<bool>
    {
        public int RequestID    { get; set; }
        public ApproveRequestByDepLeaderCommand(int requestID)
        {
            RequestID = requestID;  
        }
    }
}
