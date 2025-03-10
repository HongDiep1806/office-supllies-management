using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetAllRequestsForSupLeaderQueryHandler : IRequestHandler<GetAllRequestsForSupLeaderQuery, List<RequestDto>>
    {
        private readonly IRequestService _requestService;
        public GetAllRequestsForSupLeaderQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;   
        }
        public async Task<List<RequestDto>> Handle(GetAllRequestsForSupLeaderQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.GetAllRequestsForSupLeader();
        }
    }
}
