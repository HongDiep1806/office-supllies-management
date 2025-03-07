using AutoMapper;
using Office_supplies_management.DTOs.Permission;
using Office_supplies_management.Models;

namespace Office_supplies_management.Mappings
{
    public class PermissionProfile:Profile
    {
        public PermissionProfile() 
        {
            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();
        }
    }
}
