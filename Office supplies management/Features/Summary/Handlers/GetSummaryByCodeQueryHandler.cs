using MediatR;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetSummaryByCodeQueryHandler : IRequestHandler<GetSummaryByCodeQuery, SummaryDto>
    {
        private readonly ISummaryService _summaryService;

        public GetSummaryByCodeQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<SummaryDto> Handle(GetSummaryByCodeQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetSummaryByCodeAsync(request.SummaryCode);
        }
    }
}
