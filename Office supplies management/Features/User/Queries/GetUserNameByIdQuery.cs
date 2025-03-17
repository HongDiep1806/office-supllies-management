using MediatR;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetUserNameByIdQuery: IRequest<string>
    {
        public int Id { get; set; }
        public GetUserNameByIdQuery(int id)
        {
            Id = id;
        }
    }
}
