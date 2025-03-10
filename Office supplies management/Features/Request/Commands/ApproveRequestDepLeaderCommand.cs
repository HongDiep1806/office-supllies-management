using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class ApproveRequestDepLeaderCommand : IRequest<bool>
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }

        public ApproveRequestDepLeaderCommand(int requestId, int userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }
}
