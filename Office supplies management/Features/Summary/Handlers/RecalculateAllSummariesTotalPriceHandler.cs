using MediatR;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Commands
{
    public class RecalculateAllSummariesTotalPriceHandler : IRequestHandler<RecalculateAllSummariesTotalPriceCommand, bool>
    {
        private readonly ISummaryService _summaryService;

        public RecalculateAllSummariesTotalPriceHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<bool> Handle(RecalculateAllSummariesTotalPriceCommand request, CancellationToken cancellationToken)
        {
            return await _summaryService.RecalculateAllSummariesTotalPrice();
        }
    }
}
