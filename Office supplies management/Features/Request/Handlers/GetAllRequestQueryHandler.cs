using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetAllRequestQueryHandler : IRequestHandler<GetAllRequestQuery, List<RequestDto>>
    {
        private readonly IRequestService _requestService;
        public GetAllRequestQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<List<RequestDto>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.GetAll();  
        }
    }
}
