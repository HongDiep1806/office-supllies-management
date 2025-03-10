using MediatR;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Repositories;
using Office_supplies_management.Services; // Replace with the correct namespace for your repository

public class GetRequestsByDepartmentQueryHandler : IRequestHandler<GetRequestsByDepartmentQuery, List<RequestDto>>
{
    private readonly IRequestService _requestService;

    public GetRequestsByDepartmentQueryHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<List<RequestDto>> Handle(GetRequestsByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestService.GetByDepartment(request.DepartmentName);
        if (requests.Any())
        {
            return requests;
        }
        return null;
    }
}
