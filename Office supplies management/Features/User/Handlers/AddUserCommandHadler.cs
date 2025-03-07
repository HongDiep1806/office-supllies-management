using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Models;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class AddUserCommandHadler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IUserService _userService;
        public AddUserCommandHadler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
           return await _userService.Create(request.createRequest);
        }
    }
}
