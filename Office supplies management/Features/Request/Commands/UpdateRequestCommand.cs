using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Features.Request.Commands
{
    public class UpdateRequestCommand: IRequest<bool>
    {
        public UpdateRequestDto request { get; set; }
        public UpdateRequestCommand(UpdateRequestDto updateRequest)
        {
            request = updateRequest;
        }
    }
}
