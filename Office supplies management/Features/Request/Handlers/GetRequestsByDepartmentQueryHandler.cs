using MediatR;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Repositories; // Replace with the correct namespace for your repository

public class GetRequestsByDepartmentQueryHandler : IRequestHandler<GetRequestsByDepartmentQuery, List<RequestDto>>
{
    private readonly IRequestRepository _requestRepository;

    public GetRequestsByDepartmentQueryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<List<RequestDto>> Handle(GetRequestsByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestRepository.GetRequestsByDepartmentAsync(request.DepartmentName, cancellationToken);

        return requests.Select(r => new RequestDto
        {
            RequestID = r.RequestID,
            TotalPrice = r.TotalPrice,
            RequestCode = r.RequestCode,
            CreatedDate = r.CreatedDate,
            IsApprovedByDepLead = r.IsApprovedByDepLead,
            IsApprovedBySupLead = r.IsApprovedBySupLead,
            UserID = r.UserID,
            Product_Requests = r.Product_Requests?.Select(pr => new ProductRequestDto
            {
                Product_RequestID = pr.Product_RequestID,
                ProductID = pr.ProductID,
                Quantity = pr.Quantity
            }).ToList() ?? new List<ProductRequestDto>()
        }).ToList();
    }
}
