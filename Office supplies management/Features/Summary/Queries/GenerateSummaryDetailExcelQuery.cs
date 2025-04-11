using MediatR;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GenerateSummaryDetailExcelQuery : IRequest<byte[]>
    {
        public int SummaryId { get; set; }

        public GenerateSummaryDetailExcelQuery(int summaryId)
        {
            SummaryId = summaryId;
        }
    }
}
