// File: Features/Summary/Handlers/GetDepartmentUsageReportQueryHandler.cs
using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services; // Add this using directive
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetDepartmentUsageReportHandler : IRequestHandler<GetDepartmentUsageReportQuery, decimal>
    {
        private readonly ISummaryService _summaryService;

        public GetDepartmentUsageReportHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<decimal> Handle(GetDepartmentUsageReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _summaryService.GetDepartmentUsageReport(request.Department, request.StartDate, request.EndDate);
            return report.Sum(r => r.TotalAmount);
        }
    }
}
