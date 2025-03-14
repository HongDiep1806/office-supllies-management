using MediatR;
using Office_supplies_management.Services;

public class GetRequestsInApprovedSummaryHandler : IRequestHandler<GetRequestsInApprovedSummaryQuery, List<RequestDto>>
{
    private readonly IRequestService _requestService;

    public GetRequestsInApprovedSummaryHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<List<RequestDto>> Handle(GetRequestsInApprovedSummaryQuery request, CancellationToken cancellationToken)
    {
        return await _requestService.GetRequestsInApprovedSummary();
    }
}
