using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class DeleteRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteRequestCommand(int id)
        {
            Id = id;    
        }
    }
}
