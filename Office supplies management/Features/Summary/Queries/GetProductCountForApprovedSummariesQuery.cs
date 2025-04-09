using MediatR;
using System;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetProductCountForApprovedSummariesQuery : IRequest<Dictionary<string, int>>, IBaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetProductCountForApprovedSummariesQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
