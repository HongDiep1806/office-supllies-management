using Office_supplies_management.DTOs.Permission;

namespace Office_supplies_management.Services
{
    public interface IUserType_PermissionService
    {
        Task<List<PermissionDto>> GetPermissionListByUserTypeIDAsync(int usertypeId);
    }
}
