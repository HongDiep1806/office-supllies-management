using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class ResetApprovalDatesCommandHandler : IRequestHandler<ResetApprovalDatesCommand, bool>
    {
        private readonly IRequestService _requestService;

        public ResetApprovalDatesCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(ResetApprovalDatesCommand request, CancellationToken cancellationToken)
        {
            // Delegate the task to the service
            return await _requestService.ResetApprovalDatesAsync();
        }
    }
}
