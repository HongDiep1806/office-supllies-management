using MediatR;
using Office_supplies_management.DTOs.Notification;
using Office_supplies_management.Features.Notification.Queries;
using Office_supplies_management.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Notification.Handlers
{
    public class GetNotificationsByUserIDHandler : IRequestHandler<GetNotificationsByUserIDQuery, List<NotificationDto>>
    {
        private readonly INotificationService _notificationService;

        public GetNotificationsByUserIDHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<List<NotificationDto>> Handle(GetNotificationsByUserIDQuery request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetNotificationsByUserID(request.UserId);
        }
    }
}
