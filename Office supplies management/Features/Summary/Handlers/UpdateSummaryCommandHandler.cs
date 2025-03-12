using MediatR;
using Office_supplies_management.DTOs.Request;

public class UpdateSummaryCommandHandler : IRequestHandler<UpdateSummaryCommand, bool>
{
    private readonly ISummaryService _summaryService;

    public UpdateSummaryCommandHandler(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    public async Task<bool> Handle(UpdateSummaryCommand request, CancellationToken cancellationToken)
    {
        var updateSummaryDto = new UpdateSummaryDto
        {
            SummaryID = request.SummaryID,
            IsProcessedBySupLead = request.IsProcessedBySupLead,
            IsApprovedBySupLead = request.IsApprovedBySupLead
        };

        return await _summaryService.UpdateSummary(updateSummaryDto);
    }
}