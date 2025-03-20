using AutoMapper;
using Office_supplies_management.DTOs.Notification;
using Office_supplies_management.Models;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationDto>();
    }
}
