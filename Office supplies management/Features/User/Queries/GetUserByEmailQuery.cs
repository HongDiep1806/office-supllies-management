using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetUserByEmailQuery :IRequest<UserDto>
    {
        public string email;
        public GetUserByEmailQuery(string email)
        {
            this.email = email;
        }
    }
}
