using MediatR;
using Office_supplies_management.Features.Notification.Commands;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Notification.Handlers
{
    public class GetUnreadNotificationCountHandler : IRequestHandler<GetUnreadNotificationCountCommand, int>
    {
        private readonly INotificationService _notificationService;

        public GetUnreadNotificationCountHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<int> Handle(GetUnreadNotificationCountCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetUnreadNotificationCountByUserAsync(request.UserId);
        }
    }
}
