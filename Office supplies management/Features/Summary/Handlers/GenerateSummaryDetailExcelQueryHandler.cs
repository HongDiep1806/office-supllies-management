using ClosedXML.Excel;
using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GenerateSummaryDetailExcelQueryHandler : IRequestHandler<GenerateSummaryDetailExcelQuery, byte[]>
    {
        private readonly ISummaryService _summaryService;

        public GenerateSummaryDetailExcelQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<byte[]> Handle(GenerateSummaryDetailExcelQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GenerateSummaryDetailExcel(request.SummaryId);
        }
    }
}
