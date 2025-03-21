using MediatR;
using Office_supplies_management.DTOs.Notification;
using Office_supplies_management.Features.Notification.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Notification.Handlers
{
    public class GetUnreadNotificationsByUserQueryHandler : IRequestHandler<GetUnreadNotificationsByUserQuery, List<NotificationDto>>
    {
        private readonly INotificationService _notificationService;

        public GetUnreadNotificationsByUserQueryHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<List<NotificationDto>> Handle(GetUnreadNotificationsByUserQuery request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetUnreadNotificationsByUserAsync(request.UserId);
        }
    }
}
