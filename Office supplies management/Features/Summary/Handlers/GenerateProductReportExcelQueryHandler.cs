using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GenerateProductReportExcelQueryHandler : IRequestHandler<GenerateProductReportExcelQuery, byte[]>
    {
        private readonly ISummaryService _summaryService;

        public GenerateProductReportExcelQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<byte[]> Handle(GenerateProductReportExcelQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GenerateProductReportExcel(request.StartDate, request.EndDate);
        }
    }
}
