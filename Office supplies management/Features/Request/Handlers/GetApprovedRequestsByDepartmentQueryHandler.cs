using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetApprovedRequestsByDepartmentQueryHandler : IRequestHandler<GetApprovedRequestsByDepartmentQuery, List<RequestDto>>
    {
        private readonly IRequestService _requestService;

        public GetApprovedRequestsByDepartmentQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<List<RequestDto>> Handle(GetApprovedRequestsByDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await _requestService.GetApprovedRequestsByDepartment(request.Department);
        }
    }
}
