using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetApprovedSummariesWithRequestsQueryHandler : IRequestHandler<GetApprovedSummariesWithRequestsQuery, Dictionary<int, List<RequestDto>>>
    {
        private readonly ISummaryService _summaryService;

        public GetApprovedSummariesWithRequestsQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<Dictionary<int, List<RequestDto>>> Handle(GetApprovedSummariesWithRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetApprovedSummariesWithRequests();
        }
    }
}
