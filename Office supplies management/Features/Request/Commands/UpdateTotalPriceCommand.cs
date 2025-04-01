using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class UpdateTotalPriceCommand : IRequest<bool>
    {
        public int RequestID { get; set; }

        public UpdateTotalPriceCommand(int requestID)
        {
            RequestID = requestID;
        }
    }
}
