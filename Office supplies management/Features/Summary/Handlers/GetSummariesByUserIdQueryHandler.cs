// File: Features/Summary/Handlers/GetSummariesByUserIdQueryHandler.cs
using MediatR;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Services;
using Office_supplies_management.Features.Summary.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetSummariesByUserIdQueryHandler : IRequestHandler<GetSummariesByUserIdQuery, List<SummaryDto>>
    {
        private readonly ISummaryService _summaryService;

        public GetSummariesByUserIdQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<List<SummaryDto>> Handle(GetSummariesByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetSummariesByUserId(request.UserId);
        }
    }
}
