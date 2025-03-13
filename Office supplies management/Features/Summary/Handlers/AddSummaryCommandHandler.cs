using MediatR;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Features.Summary.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class AddSummaryCommandHandler : IRequestHandler<AddSummaryCommand, SummaryDto>
    {
        private readonly ISummaryService _summaryService;
        public AddSummaryCommandHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }
        public async Task<SummaryDto> Handle(AddSummaryCommand request, CancellationToken cancellationToken)
        {
            return await _summaryService.CreateSummary(request.request);
        }
    }
}
