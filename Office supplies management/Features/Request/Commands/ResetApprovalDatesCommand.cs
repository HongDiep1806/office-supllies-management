using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class ResetApprovalDatesCommand : IRequest<bool>
    {
        // No parameters needed as this applies to all requests
    }
}
