using MediatR;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

public class UpdateTotalPriceHandler : IRequestHandler<UpdateTotalPriceCommand, bool>
{
    private readonly IRequestService _requestService;

    public UpdateTotalPriceHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<bool> Handle(UpdateTotalPriceCommand command, CancellationToken cancellationToken)
    {
        return await _requestService.RecalculateTotalPrice(command.RequestID);
    }
}