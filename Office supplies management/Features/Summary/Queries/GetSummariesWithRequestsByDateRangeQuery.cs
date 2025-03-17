using MediatR;
using Office_supplies_management.DTOs.Request;
using System;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetSummariesWithRequestsByDateRangeQuery : IRequest<List<RequestDto>>
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public GetSummariesWithRequestsByDateRangeQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
