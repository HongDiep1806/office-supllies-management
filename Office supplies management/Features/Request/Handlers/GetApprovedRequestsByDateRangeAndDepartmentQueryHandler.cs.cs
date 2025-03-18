using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetApprovedRequestsByDateRangeAndDepartmentQueryHandler : IRequestHandler<GetApprovedRequestsByDateRangeAndDepartmentQuery, List<RequestDto>>
    {
        private readonly IRequestService _requestService;

        public GetApprovedRequestsByDateRangeAndDepartmentQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<List<RequestDto>> Handle(GetApprovedRequestsByDateRangeAndDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.GetApprovedRequestsByDateRangeAndDepartment(request.StartDate, request.EndDate, request.Department);
        }
    }
}

