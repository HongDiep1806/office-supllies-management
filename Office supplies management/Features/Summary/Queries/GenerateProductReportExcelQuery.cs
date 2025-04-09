using MediatR;
using System;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GenerateProductReportExcelQuery : IRequest<byte[]>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GenerateProductReportExcelQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
