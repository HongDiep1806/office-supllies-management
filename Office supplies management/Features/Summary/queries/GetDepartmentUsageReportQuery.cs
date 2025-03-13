using MediatR;
using System;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetDepartmentUsageReportQuery : IRequest<decimal>
    {
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}