using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class ApproveRequestByDepLeaderCommandHandler : IRequestHandler<ApproveRequestByDepLeaderCommand, bool>
    {
        private readonly IRequestService _requestService;
        public ApproveRequestByDepLeaderCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<bool> Handle(ApproveRequestByDepLeaderCommand request, CancellationToken cancellationToken)
        {
            return await _requestService.ApproveByDepLeader(request.RequestID, request.Note);
        }
    }
}