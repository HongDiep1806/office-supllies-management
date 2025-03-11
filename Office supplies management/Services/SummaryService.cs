
using AutoMapper;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        public SummaryService(ISummaryRepository summaryRepository, IRequestRepository requestRepository, IMapper mapper)
        {
            _summaryRepository = summaryRepository;
            _requestRepository = requestRepository;
            _mapper = mapper;
        }
        public async Task<SummaryDto> CreateSummary(CreateSummaryDto createSummaryDto)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsOfSummary = requests.Where(r => createSummaryDto.RequestIDs.Contains(r.RequestID)).ToList();
            var newSummary = new Summary
            {
                UserID = createSummaryDto.UserID,
                Requests = requestsOfSummary,
                TotalPrice = requestsOfSummary.Sum(r => r.TotalPrice)
            };

           await _summaryRepository.CreateAsync(newSummary);
            foreach (var request in requestsOfSummary)
            {
                request.SummaryID = newSummary.SummaryID;
                await _requestRepository.UpdateAsync(request.RequestID, request);
            }
            return _mapper.Map<SummaryDto>(newSummary);
        }
    }
}
