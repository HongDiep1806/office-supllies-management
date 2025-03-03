using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class AddRequestCommandHandler : IRequestHandler<AddRequestCommand, RequestDto>
    {
        private readonly IRequestService _requestService;
        public AddRequestCommandHandler(IRequestService requestService)
        {
            _requestService = requestService; 
        }
        public async Task<RequestDto> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
            return await _requestService.Create(request.createRequest);
        }
    }
}
