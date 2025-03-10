using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class ApproveRequestFinEmployeeCommand:IRequest<bool>
    {
        public int RequestId { get; set; }

        public ApproveRequestFinEmployeeCommand(int requestId)
        {
            RequestId = requestId;

        }
    }
}
