using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Features.Request.Commands
{
    public class AddRequestCommand : IRequest<RequestDto>
    {
        public CreateRequestDto createRequest { get; set; }
        public AddRequestCommand(CreateRequestDto createRequest)
        {
            this.createRequest = createRequest;
        }
    }
}
