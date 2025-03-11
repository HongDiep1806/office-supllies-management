using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class NotApproveRequestByFinEmployeeCommand : IRequest<bool>
    {
        public int RequestID { get; set; }
        public NotApproveRequestByFinEmployeeCommand(int requestID)
        {
            RequestID = requestID;
        }
    }
}
