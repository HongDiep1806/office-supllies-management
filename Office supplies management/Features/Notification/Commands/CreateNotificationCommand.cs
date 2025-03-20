using MediatR;
using Office_supplies_management.DTOs.Notification;

namespace Office_supplies_management.Features.Notification.Commands
{
    public class CreateNotificationCommand : IRequest<NotificationDto>
    {
        public CreateNotificationDto CreateNotificationDto { get; }

        public CreateNotificationCommand(CreateNotificationDto createNotificationDto)
        {
            CreateNotificationDto = createNotificationDto;
        }
    }
}
