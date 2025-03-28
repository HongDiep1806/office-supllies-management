using MediatR;

namespace Office_supplies_management.Features.Notification.Commands
{
    public class GetUnreadNotificationCountCommand : IRequest<int>
    {
        public int UserId { get; set; }

        public GetUnreadNotificationCountCommand(int userId)
        {
            UserId = userId;
        }
    }
}