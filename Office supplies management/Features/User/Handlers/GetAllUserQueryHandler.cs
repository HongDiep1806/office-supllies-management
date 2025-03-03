using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IUserService _userService;
        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAll();
        }
    }
}
