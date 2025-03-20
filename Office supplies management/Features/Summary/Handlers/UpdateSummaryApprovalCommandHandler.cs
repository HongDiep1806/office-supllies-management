using MediatR;
using Office_supplies_management.Features.Summary.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class UpdateSummaryApprovalCommandHandler : IRequestHandler<UpdateSummaryApprovalCommand, bool>
    {
        private readonly ISummaryService _summaryService;

        public UpdateSummaryApprovalCommandHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<bool> Handle(UpdateSummaryApprovalCommand request, CancellationToken cancellationToken)
        {
            return await _summaryService.UpdateSummaryApprovalAsync(request.SummaryId, request.IsApproved);
        }
    }
}
