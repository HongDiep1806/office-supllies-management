using MediatR;

namespace Office_supplies_management.Features.Notification.Commands
{
    public class MarkAsReadCommand : IRequest<bool>
    {
        public int NotificationId { get; }

        public MarkAsReadCommand(int notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
