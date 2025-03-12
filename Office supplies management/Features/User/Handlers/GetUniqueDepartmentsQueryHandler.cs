using MediatR;
using Office_supplies_management.Features.User.Queries;
using Office_supplies_management.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.User.Handlers
{
    public class GetUniqueDepartmentsQueryHandler : IRequestHandler<GetUniqueDepartmentsQuery, List<string>>
    {
        private readonly IUserService _userService;

        public GetUniqueDepartmentsQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<string>> Handle(GetUniqueDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUniqueDepartments();
        }
    }
}
