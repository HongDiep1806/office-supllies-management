using AutoMapper;
using Office_supplies_management.DAL;
using Office_supplies_management.DTOs.Permission;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserType_PermissionService _userTypePermissionService;

        public UserService(IUserType_PermissionService userTypePermissionService, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userTypePermissionService = userTypePermissionService;
        }

        public async Task<UserDto> Create(CreateUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            newUser.Password = PasswordHashingService.HashPassword(dto.Password);
            await _userRepository.CreateAsync(newUser);
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<PermissionDto>> GetAllPermissions(int userTypeId)
        {
            var permissions = await _userTypePermissionService.GetPermissionListByUserTypeIDAsync(userTypeId);
            if (permissions == null)
            {
                throw new Exception("Permissions not found for UserTypeId: " + userTypeId);
            }
            return permissions;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var users = await _userRepository.GetAllAsync();
            var currentUser = users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            if (currentUser == null)
            {
                throw new Exception("khong tim dc user"); // Trả về null thay vì gây lỗi mapping
            }
            //Console.WriteLine("ID: " + currentUser.UserID);
            return _mapper.Map<UserDto>(currentUser);
        }

        public async Task<UserDto> GetById(int id)
        {
            //Console.WriteLine($"🔍 Fetching user by ID: {id}");
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                //Console.WriteLine($"❌ User not found for ID: {id}");
                throw new Exception("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }


        public async Task<List<UserDto>> GetUsersByDepartment(string department)
        {
            var users = await _userRepository.GetAllAsync();
            var usersInDepartment = users.Where(u => u.Department == department).ToList();
            return _mapper.Map<List<UserDto>>(usersInDepartment);
        }
        public async Task<List<string>> GetUniqueDepartments()
        {
            var users = await _userRepository.GetAllAsync();
            var departments = users.Select(u => u.Department).Distinct().ToList();
            return departments;
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }
    }
}
