using MediatR;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Commands
{
    public class RecalculateAllRequestsTotalPriceHandler : IRequestHandler<RecalculateAllRequestsTotalPriceCommand, bool>
    {
        private readonly IRequestService _requestService;

        public RecalculateAllRequestsTotalPriceHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(RecalculateAllRequestsTotalPriceCommand request, CancellationToken cancellationToken)
        {
            return await _requestService.RecalculateAllRequestsTotalPrice();
        }
    }
}
