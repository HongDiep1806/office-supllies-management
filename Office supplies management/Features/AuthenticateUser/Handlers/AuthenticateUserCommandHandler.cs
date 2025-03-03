using MediatR;
using Office_supplies_management.Features.AuthenticateUser.Commands;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.AuthenticateUser.Handlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        public AuthenticateUserCommandHandler(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }
        public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userService.GetByEmail(request.loginRequest.Email);
            var isMatched = PasswordHashingService.VerifyPassword(request.loginRequest.Password, currentUser.Password);
            if (isMatched)
            {
                var token = await _jwtService.GenerateToken(currentUser.UserID);
                return token;
            }
            return null;
        }
    }
}
