using MediatR;
using Office_supplies_management.Features.User.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.User.Handlers
{
    public class GetUserNameByIdQueryHandler : IRequestHandler<GetUserNameByIdQuery, string>
    {
        private readonly IUserService _userService;
        public GetUserNameByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<string> Handle(GetUserNameByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetNameById(request.Id);
        }
    }
}
