using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class NotApproveRequestByFinEmployeeHandler : IRequestHandler<NotApproveRequestByFinEmployeeCommand, bool>
    {
        private readonly IRequestService _requestService;

        public NotApproveRequestByFinEmployeeHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(NotApproveRequestByFinEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await _requestService.NotApproveRequestByFinEmployee(command.RequestID, command.Note);
        }
    }
}
