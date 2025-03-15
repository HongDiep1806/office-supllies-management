using AutoMapper;
using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetRequestsInDateRangeQueryHandler : IRequestHandler<GetRequestsInDateRangeQuery, List<RequestDto>>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetRequestsInDateRangeQueryHandler(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<List<RequestDto>> Handle(GetRequestsInDateRangeQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestRepository.GetAllInclude(r => r.Product_Requests);
            var filteredRequests = requests
                .Where(r => r.CreatedDate.Date >= request.StartDate && r.CreatedDate.Date <= request.EndDate && r.IsSummaryBeApproved)
                .ToList();

            return _mapper.Map<List<RequestDto>>(filteredRequests);
        }
    }
}
