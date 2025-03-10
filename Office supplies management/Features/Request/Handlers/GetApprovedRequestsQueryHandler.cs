using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetApprovedRequestsQueryHandler : IRequestHandler<GetApprovedRequestsQuery, List<RequestDto>>
    {
        private readonly IRequestService _requestService;

        public GetApprovedRequestsQueryHandler(IRequestService requestService)
        {
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        public async Task<List<RequestDto>> Handle(GetApprovedRequestsQuery request, CancellationToken cancellationToken)
        {
            var approvedRequests = await _requestService.GetApprovedRequestsByDepLeader();
            return approvedRequests;
        }
    }
}
