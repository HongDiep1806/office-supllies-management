using AutoMapper;
using Office_supplies_management.DTOs.Permission;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public async Task<PermissionDto> GetById(int id)
        {
            return _mapper.Map<PermissionDto>(await _permissionRepository.GetByIdAsync(id));
        }
    }
}
