// File: Features/Summary/Queries/GetRequestsBySummaryIdQuery.cs
using MediatR;
using Office_supplies_management.DTOs.Request;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetRequestsBySummaryIdQuery : IRequest<List<RequestDto>>
    {
        public int SummaryId { get; set; }

        public GetRequestsBySummaryIdQuery(int summaryId)
        {
            SummaryId = summaryId;
        }
    }
}
