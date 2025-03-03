using Office_supplies_management.DTOs.UserType;
using Office_supplies_management.DTOs.Permission;

namespace Office_supplies_management.Services
{
    public interface IPermissionService
    {
        Task<PermissionDto> GetById(int id);

    }
}
