using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class NotApproveRequestByDepLeaderHandler : IRequestHandler<NotApproveRequestByDepLeaderCommand, bool>
    {
        private readonly IRequestService _requestService;

        public NotApproveRequestByDepLeaderHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(NotApproveRequestByDepLeaderCommand command, CancellationToken cancellationToken)
        {
            return await _requestService.NotApproveRequestByDepLeader(command.RequestID);
        }
    }
}
