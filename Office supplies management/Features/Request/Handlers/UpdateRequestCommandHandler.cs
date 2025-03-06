using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, bool>
    {
        private readonly IRequestService _requestService;
        public UpdateRequestCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<bool> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            return await _requestService.Update(request.request);
        }
    }
}
