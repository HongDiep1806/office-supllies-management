using MediatR;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class CountRequestsQueryHandler : IRequestHandler<CountRequestsQuery, int>
    {
        private readonly IRequestService _requestService;
        public CountRequestsQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<int> Handle(CountRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.Count();
        }
    }
}
