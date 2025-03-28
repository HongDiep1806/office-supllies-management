using AutoMapper;
using Office_supplies_management.DTOs.Notification;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office_supplies_management.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> GetNotificationsByUserID(int userId)
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var userNotifications = notifications.Where(n => n.UserID == userId).ToList();
            return _mapper.Map<List<NotificationDto>>(userNotifications);
        }

        public async Task<NotificationDto> CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var newNotification = new Notification
            {
                UserID = createNotificationDto.UserID,
                Message = createNotificationDto.Message,
                IsRead = false,
                CreatedDate = DateTime.Now,
                RequestID = createNotificationDto.RequestID,
                Sender = createNotificationDto.Sender
            };
            await _notificationRepository.CreateAsync(newNotification);
            return _mapper.Map<NotificationDto>(newNotification);
        }


        public async Task<bool> MarkAsRead(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification == null)
            {
                return false;
            }

            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notificationId, notification);
            return true;
        }
        public async Task<List<NotificationDto>> GetUnreadNotificationsByUserAsync(int userId)
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var unreadNotifications = notifications.Where(n => n.UserID == userId && !n.IsRead).ToList();
            return _mapper.Map<List<NotificationDto>>(unreadNotifications);
        }
        public async Task<bool> MarkAllAsReadAsync(int userId)
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var userNotifications = notifications.Where(n => n.UserID == userId && !n.IsRead).ToList();

            foreach (var notification in userNotifications)
            {
                notification.IsRead = true;
                await _notificationRepository.UpdateAsync(notification.NotificationID, notification);
            }

            return true;
        }
        public async Task<int> GetUnreadNotificationCountByUserAsync(int userId)
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var unreadCount = notifications.Count(n => n.UserID == userId && !n.IsRead);
            return unreadCount;
        }
    }
}
