using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.AuthenticateUser.Commands
{
    public class AuthenticateUserCommand : IRequest<string>
    {
        public LoginDto loginRequest;
        public AuthenticateUserCommand(LoginDto loginDto)
        {
            loginRequest = loginDto;
        }

    }
}
