using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetRequestsByUserIDQueryHandler : IRequestHandler<GetRequestsByUserIDQuery, List<RequestDto>>
    {
        private readonly IRequestService _requestService;

        public GetRequestsByUserIDQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<List<RequestDto>> Handle(GetRequestsByUserIDQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.GetByUserID(request.UserId);
        }
    }
}
