using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.User.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.User.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserService _userService;
        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetById(request.Id);  
        }
    }
}
