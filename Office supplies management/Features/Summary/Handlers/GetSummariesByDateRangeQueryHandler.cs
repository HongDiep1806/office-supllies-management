// File: Features/Summary/Handlers/GetSummariesByDateRangeQueryHandler.cs
using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.DTOs.Summary;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetSummariesByDateRangeQueryHandler : IRequestHandler<GetSummariesByDateRangeQuery, List<SummaryDto>>
    {
        private readonly ISummaryService _summaryService;

        public GetSummariesByDateRangeQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<List<SummaryDto>> Handle(GetSummariesByDateRangeQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetSummariesByDateRange(request.StartDate, request.EndDate);
        }
    }
}
