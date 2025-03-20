using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.User.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.User.Handlers
{
    public class GetAllUsersByTypeQueryHandler : IRequestHandler<GetAllUsersByTypeQuery, List<UserDto>>
    {
        private readonly IUserService _userService;

        public GetAllUsersByTypeQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllUsersByTypeAsync(request.UserTypeID);
        }
    }
}
