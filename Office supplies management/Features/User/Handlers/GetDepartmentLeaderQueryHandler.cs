using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.User.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.User.Handlers
{
    public class GetDepartmentLeaderQueryHandler : IRequestHandler<GetDepartmentLeaderQuery, UserDto>
    {
        private readonly IUserService _userService;

        public GetDepartmentLeaderQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetDepartmentLeaderQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetDepartmentLeaderAsync(request.Department);
        }
    }
}
