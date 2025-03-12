using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class UpdateRequestStatusCommandHandler : IRequestHandler<UpdateRequestStatusCommand, bool>
    {
        private readonly IRequestService _requestService;

        public UpdateRequestStatusCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(UpdateRequestStatusCommand request, CancellationToken cancellationToken)
        {
            await _requestService.UpdateRequestStatus(request.SummaryID, request.IsProcessedBySupLead, request.IsApprovedBySupLead);
            return true;
        }
    }
}
