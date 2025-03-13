// File: Features/Summary/Handlers/GetDepartmentCostsQueryHandler.cs
using MediatR;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetDepartmentCostsQueryHandler : IRequestHandler<GetDepartmentCostsQuery, List<DepartmentCostDto>>
    {
        private readonly ISummaryService _summaryService;

        public GetDepartmentCostsQueryHandler(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<List<DepartmentCostDto>> Handle(GetDepartmentCostsQuery request, CancellationToken cancellationToken)
        {
            return await _summaryService.GetDepartmentCosts(request.StartDate, request.EndDate);
        }
    }
}
