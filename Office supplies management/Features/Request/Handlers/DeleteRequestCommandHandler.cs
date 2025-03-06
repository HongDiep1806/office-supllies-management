using MediatR;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, bool>
    {
        private readonly IRequestService _service;
        public DeleteRequestCommandHandler(IRequestService requestService)
        {
            _service = requestService;
        }
        public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteByID(request.Id);   
        }
    }
}
