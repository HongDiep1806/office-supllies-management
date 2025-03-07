using AutoMapper;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Models;


namespace Office_supplies_management.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UserDto, User>(); 
            CreateMap<User, UserDto>();
        }
    }
}
