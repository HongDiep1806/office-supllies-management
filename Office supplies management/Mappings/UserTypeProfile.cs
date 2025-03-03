using AutoMapper;
using Office_supplies_management.DTOs.UserType;
using Office_supplies_management.Models;

namespace Office_supplies_management.Mappings
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile() 
        {
            CreateMap<UserType, UserTypeDto>();
            CreateMap<UserTypeDto, UserType>();
        }

    }
}
