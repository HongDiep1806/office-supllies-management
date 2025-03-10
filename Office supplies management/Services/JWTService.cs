using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Office_supplies_management.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Office_supplies_management.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryMinutes;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserTypeService _usertypeService;

        public JwtService(IConfiguration config, IUserService userService, IMapper mapper, IUserTypeService usertypeService)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            _secret = jwtSettings["Secret"]!;
            _issuer = jwtSettings["Issuer"]!;
            _audience = jwtSettings["Audience"]!;
            _expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"]!);
            _userService = userService;
            _mapper = mapper;
            _usertypeService = usertypeService;
        }

        public async Task<string> GenerateToken(int userId)
        {
            var currentUser = _mapper.Map<User>(await _userService.GetById(userId));
            var userType = _mapper.Map<UserType>(await _usertypeService.GetById(currentUser.UserTypeID));
            var permissions = await _userService.GetAllPermissions(userType.UserTypeID);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Use NameIdentifier
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // Keep both for compatibility
                 // Check if this value is valid
                new Claim(JwtRegisteredClaimNames.Email, currentUser.Email),
                new Claim(JwtRegisteredClaimNames.Name, currentUser.FullName),
                new Claim("Role", userType.Type),
                new Claim("Department", currentUser.Department)
            };

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission.Description));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_issuer, _audience, claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryMinutes), signingCredentials: creds);

            // **Log the claims to debug**
            Console.WriteLine("Generated JWT Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
