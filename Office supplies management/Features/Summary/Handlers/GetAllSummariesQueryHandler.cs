using MediatR;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Services;
using Office_supplies_management.Features.Summary.Queries; // Add this line to include the correct namespace

public class GetAllSummariesQueryHandler : IRequestHandler<GetAllSummariesQuery, List<SummaryDto>>
{
    private readonly ISummaryService _summaryService;

    public GetAllSummariesQueryHandler(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    public async Task<List<SummaryDto>> Handle(GetAllSummariesQuery request, CancellationToken cancellationToken)
    {
        return await _summaryService.GetAllSummaries();
    }
}
