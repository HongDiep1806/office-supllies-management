using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetNumberOfTotalSummariesQueryHandler : IRequestHandler<GetNumberOfTotalSummariesQuery, int>
    {
        private readonly ISummaryService _summaryService;
        public GetNumberOfTotalSummariesQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }
        public async Task<int> Handle(GetNumberOfTotalSummariesQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.CountSummaries();
        }
    }
}
