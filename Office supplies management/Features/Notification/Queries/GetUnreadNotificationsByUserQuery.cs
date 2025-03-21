using MediatR;
using Office_supplies_management.DTOs.Notification;

namespace Office_supplies_management.Features.Notification.Queries
{
    public class GetUnreadNotificationsByUserQuery : IRequest<List<NotificationDto>>
    {
        public int UserId { get; set; }
    }
}
