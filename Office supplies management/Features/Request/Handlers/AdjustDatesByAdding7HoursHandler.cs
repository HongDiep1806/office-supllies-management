using MediatR;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Commands
{
    public class AdjustDatesByAdding7HoursHandler : IRequestHandler<AdjustDatesByAdding7HoursCommand, bool>
    {
        private readonly IRequestService _requestService;

        public AdjustDatesByAdding7HoursHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<bool> Handle(AdjustDatesByAdding7HoursCommand request, CancellationToken cancellationToken)
        {
            return await _requestService.AdjustDatesByAdding7Hours();
        }
    }
}
