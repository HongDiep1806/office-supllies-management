using MediatR;

public class UpdateSummaryCommand : IRequest<bool>
{
    public int SummaryID { get; set; }
    public bool IsProcessedBySupLead { get; set; }
    public bool IsApprovedBySupLead { get; set; }
}