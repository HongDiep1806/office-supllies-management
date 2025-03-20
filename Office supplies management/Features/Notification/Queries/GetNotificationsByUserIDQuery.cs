using MediatR;
using Office_supplies_management.DTOs.Notification;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Notification.Queries
{
    public class GetNotificationsByUserIDQuery : IRequest<List<NotificationDto>>
    {
        public int UserId { get; }

        public GetNotificationsByUserIDQuery(int userId)
        {
            UserId = userId;
        }
    }
}
