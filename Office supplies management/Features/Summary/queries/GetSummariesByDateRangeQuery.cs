// File: Features/Summary/Queries/GetSummariesByDateRangeQuery.cs
using MediatR;
using Office_supplies_management.DTOs.Summary;
using System;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetSummariesByDateRangeQuery : IRequest<List<SummaryDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
