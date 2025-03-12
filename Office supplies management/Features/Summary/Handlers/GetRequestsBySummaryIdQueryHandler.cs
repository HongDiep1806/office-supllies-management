// File: Features/Summary/Handlers/GetRequestsBySummaryIdQueryHandler.cs
using AutoMapper;
using MediatR;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Summary.Queries;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Summary.Handlers
{
    public class GetRequestsBySummaryIdQueryHandler : IRequestHandler<GetRequestsBySummaryIdQuery, List<RequestDto>>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetRequestsBySummaryIdQueryHandler(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<List<RequestDto>> Handle(GetRequestsBySummaryIdQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests.Where(r => r.SummaryID == request.SummaryId).ToList();
            return _mapper.Map<List<RequestDto>>(filteredRequests);
        }
    }
}
