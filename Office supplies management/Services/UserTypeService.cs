using AutoMapper;
using Office_supplies_management.DTOs.Permission;
using Office_supplies_management.DTOs.UserType;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUserType_PermissionService _userType_PermissionService;
        private readonly IUserTypeRepository _userTypeRepository;
        public UserTypeService(IUserTypeRepository userTypeRepository, IMapper mapper, IUserType_PermissionService userType_PermissionService) 
        { 
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
            _userType_PermissionService = userType_PermissionService;   
        }
        public async Task<UserTypeDto> GetById(int id)
        {
            return _mapper.Map<UserTypeDto>(await _userTypeRepository.GetByIdAsync(id));  
        }

        public async Task<List<PermissionDto>> GetPermission(int usertypeId)
        {
            return await _userType_PermissionService.GetPermissionListByUserTypeIDAsync(usertypeId);
        }
    }
}
