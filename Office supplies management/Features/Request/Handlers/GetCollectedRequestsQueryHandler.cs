using AutoMapper;
using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Queries;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Features.Request.Handlers
{
    public class GetCollectedRequestsQueryHandler : IRequestHandler<GetCollectedRequestsQuery, List<RequestDto>>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetCollectedRequestsQueryHandler(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<List<RequestDto>> Handle(GetCollectedRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestRepository.GetAllAsync();
            var collectedRequests = requests.Where(r => r.IsCollectedInSummary).ToList();
            return _mapper.Map<List<RequestDto>>(collectedRequests);
        }
    }
}
