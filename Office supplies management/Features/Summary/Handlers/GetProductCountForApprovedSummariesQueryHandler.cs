using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;

public class GetProductCountForApprovedSummariesQueryHandler : IRequestHandler<GetProductCountForApprovedSummariesQuery, Dictionary<string, int>>
{
    private readonly ISummaryService _summaryService;

    public GetProductCountForApprovedSummariesQueryHandler(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    public async Task<Dictionary<string, int>> Handle(GetProductCountForApprovedSummariesQuery request, CancellationToken cancellationToken)
    {
        var productCounts = await _summaryService.GetProductCountForApprovedSummariesInDateRange(request.StartDate, request.EndDate);

        // Assuming productCounts is a Dictionary<string, int>, adjust the transformation logic
        var formattedProductCounts = productCounts
            .Where(pc => !string.IsNullOrEmpty(pc.Key) && pc.Value > 0)
            .ToDictionary(pc => pc.Key, pc => pc.Value);

        return formattedProductCounts;
    }

}
