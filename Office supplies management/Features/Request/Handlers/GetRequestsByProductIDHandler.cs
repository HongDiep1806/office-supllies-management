using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetRequestsByProductIDHandler : IRequestHandler<GetRequestsByProductIDQuery, List<RequestDto>>
{
    private readonly IRequestService _requestService;

    public GetRequestsByProductIDHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<List<RequestDto>> Handle(GetRequestsByProductIDQuery query, CancellationToken cancellationToken)
    {
        return await _requestService.GetRequestsByProductID(query.ProductID);
    }
}

