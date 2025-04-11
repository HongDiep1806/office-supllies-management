using MediatR;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GenerateApprovedRequestsExcelQueryHandler : IRequestHandler<GenerateApprovedRequestsExcelQuery, byte[]>
    {
        private readonly ISummaryService _summaryService;

        public GenerateApprovedRequestsExcelQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<byte[]> Handle(GenerateApprovedRequestsExcelQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GenerateApprovedRequestsExcel(request.StartDate, request.EndDate, request.Department);
        }
    }
}
