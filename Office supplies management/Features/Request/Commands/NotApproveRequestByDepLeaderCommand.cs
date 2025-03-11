using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class NotApproveRequestByDepLeaderCommand : IRequest<bool>
    {
        public int RequestID { get; set; }
        public NotApproveRequestByDepLeaderCommand(int requestID)
        {
            RequestID = requestID;
        }
    }
}
