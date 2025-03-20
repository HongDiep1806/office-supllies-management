using MediatR;

namespace Office_supplies_management.Features.Summary.Commands
{
    public class UpdateSummaryApprovalCommand : IRequest<bool>
    {
        public int SummaryId { get; set; }
        public bool IsApproved { get; set; }
    }
}
