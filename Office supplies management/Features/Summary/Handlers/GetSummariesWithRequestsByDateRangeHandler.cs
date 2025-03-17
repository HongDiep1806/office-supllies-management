using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetSummariesWithRequestsByDateRangeHandler : IRequestHandler<GetSummariesWithRequestsByDateRangeQuery, List<RequestDto>>
    {
        private readonly ISummaryService _summaryService;

        public GetSummariesWithRequestsByDateRangeHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<List<RequestDto>> Handle(GetSummariesWithRequestsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetSummariesWithRequestsByDateRange(request.StartDate, request.EndDate);
        }
    }
}
