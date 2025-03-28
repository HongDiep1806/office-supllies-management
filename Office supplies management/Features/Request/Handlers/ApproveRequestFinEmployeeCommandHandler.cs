using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Repositories;
using Office_supplies_management.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class ApproveRequestFinEmployeeCommandHandler : IRequestHandler<ApproveRequestFinEmployeeCommand, bool>
    {
        private readonly IRequestService _requestService;

        public ApproveRequestFinEmployeeCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(ApproveRequestFinEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await _requestService.ApproveByFinEmployee(command.RequestId, command.Note);
        }
    }
}