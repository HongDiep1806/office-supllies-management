using MediatR;
using Office_supplies_management.DTOs.Request;

public class GetRequestsInApprovedSummaryQuery : IRequest<List<RequestDto>>
{
    public GetRequestsInApprovedSummaryQuery() { }
}
