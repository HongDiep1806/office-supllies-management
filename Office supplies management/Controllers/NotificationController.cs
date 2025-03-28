using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Notification;
using Office_supplies_management.Features.Notification.Commands;
using Office_supplies_management.Features.Notification.Queries;
using System.Threading.Tasks;

namespace Office_supplies_management.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNotificationsByUserID(int userId)
        {
            var query = new GetNotificationsByUserIDQuery(userId);
            var notifications = await _mediator.Send(query);
            return Ok(notifications);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto createNotificationDto)
        {
            var command = new CreateNotificationCommand(createNotificationDto);
            var notification = await _mediator.Send(command);
            return Ok(notification);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpPut("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var command = new MarkAsReadCommand(notificationId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpPut("mark-all-as-read/{userId}")]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            var command = new MarkAllAsReadCommand(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("unread-by-user")]
        public async Task<IActionResult> GetUnreadNotificationsByUser([FromQuery] int userId)
        {
            var query = new GetUnreadNotificationsByUserQuery { UserId = userId };
            var notifications = await _mediator.Send(query);
            return Ok(notifications);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("unread/count/{userId}")]
        public async Task<IActionResult> GetUnreadNotificationCount(int userId)
        {
            var count = await _mediator.Send(new GetUnreadNotificationCountCommand(userId));
            return Ok(count);
        }
    }
}
