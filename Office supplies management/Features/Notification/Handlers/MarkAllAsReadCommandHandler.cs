using MediatR;
using Office_supplies_management.Features.Notification.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Notification.Handlers
{
    public class MarkAllAsReadCommandHandler : IRequestHandler<MarkAllAsReadCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public MarkAllAsReadCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(MarkAllAsReadCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.MarkAllAsReadAsync(request.UserId);
        }
    }
}
