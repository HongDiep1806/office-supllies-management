using MediatR;
using Office_supplies_management.DTOs.Notification;
using Office_supplies_management.Features.Notification.Commands;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Notification.Handlers
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
    {
        private readonly INotificationService _notificationService;

        public CreateNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.CreateNotification(request.CreateNotificationDto);
        }
    }
}
