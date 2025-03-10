using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Repositories;
using Office_supplies_management.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class ApproveRequestSupLeadCommandHandler : IRequestHandler<ApproveRequestSupLeadCommand, bool>
    {
        private readonly IRequestService _requestService;

        public ApproveRequestSupLeadCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }


        public async Task<bool> Handle(ApproveRequestSupLeadCommand command, CancellationToken cancellationToken)
        {
            if (command.UserRole != "Finance Management Employee")
            {
                return false;
            }

            var requestEntity = await _requestService.GetByID(command.RequestId);
            if (requestEntity == null || requestEntity.UserID != command.UserId)
            {
                return false;
            }
            var updateRequestDto = new UpdateRequestDto
            {
                RequestID = requestEntity.RequestID,
                CreatedDate = requestEntity.CreatedDate,
                IsApprovedByDepLead = requestEntity.IsApprovedByDepLead,
                IsApprovedBySupLead = requestEntity.IsApprovedBySupLead,
                IsProcessedByDepLead = requestEntity.IsProcessedByDepLead,
                RequestCode = requestEntity.RequestCode,
                TotalPrice = requestEntity.TotalPrice,
                UserID = requestEntity.UserID,
                Products = requestEntity.Product_Requests,
            };
            requestEntity.IsApprovedBySupLead = true;
            return await _requestService.Update(updateRequestDto);
        }
    }
}
