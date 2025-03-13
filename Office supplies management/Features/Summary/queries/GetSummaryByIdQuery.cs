// File: Features/Summary/Queries/GetSummaryByIdQuery.cs
using MediatR;
using Office_supplies_management.DTOs.Summary;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetSummaryByIdQuery : IRequest<SummaryDto>
    {
        public int SummaryId { get; }

        public GetSummaryByIdQuery(int summaryId)
        {
            SummaryId = summaryId;
        }
    }
}
