using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetRequestByIDQueryHandler : IRequestHandler<GetRequestByIDQuery, RequestDto>
    {
        private readonly IRequestService _requestService;
        public GetRequestByIDQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<RequestDto> Handle(GetRequestByIDQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.GetByID(request.Id);
        }
    }
}
