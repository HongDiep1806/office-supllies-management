using MediatR;

namespace Office_supplies_management.Features.Notification.Commands
{
    public class MarkAllAsReadCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public MarkAllAsReadCommand(int userId)
        {
            UserId = userId;
        }
    }
}
