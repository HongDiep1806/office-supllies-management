using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetUsersByDepartmentQueryHandler : IRequestHandler<GetUsersByDepartmentQuery, List<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUsersByDepartmentQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserDto>> Handle(GetUsersByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userService.GetById(request.UserId);
            if (currentUser == null)
            {
                Console.WriteLine($"❌ No user found for ID: {request.UserId}");
                throw new Exception("User not found or does not exist");
            }

            Console.WriteLine($"✅ Found user {currentUser.FullName} in department {currentUser.Department}");

            // ✅ Fetch users in the same department and return them
            var usersInDepartment = await _userService.GetUsersByDepartment(currentUser.Department);

            return usersInDepartment;
        }

    }
}
