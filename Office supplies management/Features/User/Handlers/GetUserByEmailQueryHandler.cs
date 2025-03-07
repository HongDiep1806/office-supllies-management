using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.User.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.User.Handlers
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUserService _userService;
        public GetUserByEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByEmail(request.email);
        }
    }
}
