using MediatR;
using Office_supplies_management.Features.Notification.Commands;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Notification.Handlers
{
    public class MarkAsReadHandler : IRequestHandler<MarkAsReadCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public MarkAsReadHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.MarkAsRead(request.NotificationId);
        }
    }
}
