using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetAllUsersByTypeQuery : IRequest<List<UserDto>>
    {
        public int UserTypeID { get; set; }
    }
}
