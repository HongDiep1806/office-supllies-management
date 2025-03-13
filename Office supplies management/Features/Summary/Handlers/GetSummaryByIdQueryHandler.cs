// File: Features/Summary/Handlers/GetSummaryByIdQueryHandler.cs
using MediatR;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Services;
using Office_supplies_management.Features.Summary.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetSummaryByIdQueryHandler : IRequestHandler<GetSummaryByIdQuery, SummaryDto>
    {
        private readonly ISummaryService _summaryService;

        public GetSummaryByIdQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<SummaryDto> Handle(GetSummaryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetSummaryById(request.SummaryId);
        }
    }
}
