using MediatR;

namespace Office_supplies_management.Features.Request.Commands
{
    public class UpdateRequestStatusCommand : IRequest<bool>
    {
        public int SummaryID { get; set; }
        public bool IsProcessedBySupLead { get; set; }
        public bool IsApprovedBySupLead { get; set; }
    }
}
