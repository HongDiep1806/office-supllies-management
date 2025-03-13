using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Services;

public class UpdateSummaryCommandHandler : IRequestHandler<UpdateSummaryCommand, bool>
{
    private readonly ISummaryService _summaryService;
    private readonly IRequestService _requestService;

    public UpdateSummaryCommandHandler(ISummaryService summaryService, IRequestService requestService)
    {
        _summaryService = summaryService;
        _requestService = requestService;
    }

    public async Task<bool> Handle(UpdateSummaryCommand request, CancellationToken cancellationToken)
    {
        var updateSummaryDto = new UpdateSummaryDto
        {
            SummaryID = request.SummaryID,
            IsProcessedBySupLead = request.IsProcessedBySupLead,
            IsApprovedBySupLead = request.IsApprovedBySupLead
        };

        var result = await _summaryService.UpdateSummary(updateSummaryDto);

        if (result)
        {
            await _requestService.UpdateRequestStatus(request.SummaryID, request.IsProcessedBySupLead, request.IsApprovedBySupLead);
        }

        return result;
    }
}
