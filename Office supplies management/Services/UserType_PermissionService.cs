using AutoMapper;
using Office_supplies_management.DTOs.Permission;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class UserType_PermissionService : IUserType_PermissionService
    {
        private readonly IUserType_PermissionRepository _userType_PermissionRepository;
        private readonly IPermissionService _permissionService;

        public UserType_PermissionService(IPermissionService permissionService, IUserType_PermissionRepository userType_PermissionRepository)
        {
            _userType_PermissionRepository = userType_PermissionRepository;
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> GetPermissionListByUserTypeIDAsync(int usertypeId)
        {
            Console.WriteLine("doooo");
            var permissionIDs = new List<int>();
            var allUserType_Permission = await _userType_PermissionRepository.GetAllAsync();
            permissionIDs = allUserType_Permission
                            .Where(u => u.UserTypeID == usertypeId)
                            .Select(u => u.PermissionID).ToList();

            if(permissionIDs.Count()!= 0)
            {
                Console.WriteLine("co phan tu");
            }
            var permissions = new List<PermissionDto>();
            foreach (var permissionID in permissionIDs)
            {
                permissions.Add(await _permissionService.GetById(permissionID));
                Console.WriteLine("hh: "+permissionID.ToString());
            }
            return permissions;
        }
    }
}
