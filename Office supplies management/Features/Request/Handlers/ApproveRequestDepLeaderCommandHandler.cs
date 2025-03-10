using MediatR;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class ApproveRequestDepLeaderCommandHandler : IRequestHandler<ApproveRequestDepLeaderCommand, bool>
    {
        private readonly IRequestService _requestService;
        private readonly IUserService _userService;

        public ApproveRequestDepLeaderCommandHandler(IRequestService requestService, IUserService userService)
        {
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<bool> Handle(ApproveRequestDepLeaderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var user = await _userService.GetById(request.UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var requestEntity = await _requestService.GetByID(request.RequestId);
            if (requestEntity == null)
            {
                throw new InvalidOperationException("Request not found.");
            }

            // Perform the approval logic
            requestEntity.IsApprovedByDepLead = true;

            var updateRequestDto = new UpdateRequestDto
            {
                RequestID = requestEntity.RequestID,
                TotalPrice = requestEntity.TotalPrice,
                RequestCode = requestEntity.RequestCode,
                CreatedDate = requestEntity.CreatedDate,
                IsApprovedByDepLead = requestEntity.IsApprovedByDepLead, // Fix: Add this property to UpdateRequestDto
                IsApprovedBySupLead = requestEntity.IsApprovedBySupLead,
                UserID = requestEntity.UserID,
                Products = requestEntity.Product_Requests.Select(pr => new ProductRequestDto
                {
                    Product_RequestID = pr.Product_RequestID,
                    ProductID = pr.ProductID,
                    Quantity = pr.Quantity
                }).ToList()
            };

            var result = await _requestService.Update(updateRequestDto);
            return result;
        }
    }

}
