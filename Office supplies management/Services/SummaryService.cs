
using AutoMapper;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Add logging

namespace Office_supplies_management.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SummaryService> _logger;
        public SummaryService(IUserRepository userRepository, ISummaryRepository summaryRepository, IRequestRepository requestRepository, IMapper mapper, ILogger<SummaryService> logger)
        {
            _summaryRepository = summaryRepository;
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<SummaryDto> CreateSummary(CreateSummaryDto createSummaryDto)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsOfSummary = requests.Where(r => createSummaryDto.RequestIDs.Contains(r.RequestID)).ToList();
            var newSummary = new Summary
            {
                UserID = createSummaryDto.UserID,
                Requests = requestsOfSummary,
                TotalPrice = requestsOfSummary.Sum(r => r.TotalPrice),
                IsProcessedBySupLead = true,
            };

           await _summaryRepository.CreateAsync(newSummary);
            foreach (var request in requestsOfSummary)
            {
                request.SummaryID = newSummary.SummaryID;
                request.IsCollectedInSummary = true;
                await _requestRepository.UpdateAsync(request.RequestID, request);
            }
            return _mapper.Map<SummaryDto>(newSummary);
        }

        public async Task<bool> UpdateSummary(UpdateSummaryDto updateSummaryDto)
        {
            var summary = await _summaryRepository.GetByIdAsync(updateSummaryDto.SummaryID);
            if (summary == null)
            {
                return false;
            }

            summary.IsProcessedBySupLead = updateSummaryDto.IsProcessedBySupLead;
            summary.IsApprovedBySupLead = updateSummaryDto.IsApprovedBySupLead;
            await _summaryRepository.UpdateAsync(summary.SummaryID, summary);
            return true;
        }

        public async Task<List<SummaryDto>> GetAllSummaries()
        {
            var summaries = await _summaryRepository.GetAllAsync();
            return _mapper.Map<List<SummaryDto>>(summaries);
        }

        public async Task<List<SummaryDto>> GetSummariesByUserId(int userId)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var userSummaries = summaries.Where(s => s.UserID == userId).ToList();
            return _mapper.Map<List<SummaryDto>>(userSummaries);
        }

        public async Task<SummaryDto> GetSummaryById(int summaryId)
        {
            var summaries = await _summaryRepository.GetAllInclude( s => s.Requests);
            var currentSummary = summaries.FirstOrDefault(s => s.SummaryID == summaryId);
            return _mapper.Map<SummaryDto>(currentSummary);
        }

        public async Task<List<DepartmentUsageReportDto>> GetDepartmentUsageReport(string department, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Fetching summaries between {StartDate} and {EndDate}", startDate, endDate);
            var summaries = await _summaryRepository.GetAllAsync();
            var filteredSummaries = summaries
                .Where(s => s.CreatedDate.Date >= startDate.Date && s.CreatedDate.Date <= endDate.Date && s.IsApprovedBySupLead)
                .ToList();

            _logger.LogInformation("Found {Count} summaries in the date range", filteredSummaries.Count);
            foreach (var summary in filteredSummaries)
            {
                _logger.LogInformation("SummaryID: {SummaryID}, CreatedDate: {CreatedDate}, IsApprovedBySupLead: {IsApprovedBySupLead}", summary.SummaryID, summary.CreatedDate, summary.IsApprovedBySupLead);
            }

            var summaryIds = filteredSummaries.Select(s => s.SummaryID).ToList();

            var users = await _userRepository.GetAllAsync();
            var userIds = users
                .Where(u => u.Department == department)
                .Select(u => u.UserID)
                .ToList();

            _logger.LogInformation("Found {Count} users in the department {Department}", userIds.Count, department);

            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests
                .Where(r => userIds.Contains(r.UserID) && summaryIds.Contains(r.SummaryID ?? 0))
                .ToList();

            _logger.LogInformation("Found {Count} requests matching the criteria", filteredRequests.Count);
            foreach (var request in filteredRequests)
            {
                _logger.LogInformation("RequestID: {RequestID}, UserID: {UserID}, SummaryID: {SummaryID}, TotalPrice: {TotalPrice}", request.RequestID, request.UserID, request.SummaryID, request.TotalPrice);
            }

            var report = filteredRequests
                .GroupBy(r => r.User.Department)
                .Select(g => new DepartmentUsageReportDto
                {
                    Department = g.Key,
                    TotalAmount = g.Sum(r => r.TotalPrice)
                })
                .ToList();

            return report;
        }

        public async Task<List<SummaryDto>> GetSummariesByDateRange(DateTime startDate, DateTime endDate)
        {
            var summaries = await _summaryRepository.GetAllAsync();
            var filteredSummaries = summaries
                .Where(s => s.CreatedDate.Date >= startDate && s.CreatedDate.Date <= endDate)
                .ToList();

            return _mapper.Map<List<SummaryDto>>(filteredSummaries);
        }
        public async Task<List<RequestDto>> GetRequestsBySummaryId(int summaryId)
        {
            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests.Where(r => r.SummaryID == summaryId).ToList();
            return _mapper.Map<List<RequestDto>>(filteredRequests);
        }
        public async Task<List<DepartmentCostDto>> GetDepartmentCosts(DateTime startDate, DateTime endDate)
        {
            // Get all unique departments
            var users = await _userRepository.GetAllAsync();
            var departments = users.Select(u => u.Department).Distinct().ToList();

            // Get all requests where the summary is approved and within the date range
            var requests = await _requestRepository.GetAllAsync();
            var filteredRequests = requests
                .Where(r => r.SummaryID.HasValue && r.IsSummaryBeApproved && r.CreatedDate.Date >= startDate && r.CreatedDate.Date <= endDate)
                .ToList();

            // Log the filtered requests
            foreach (var request in filteredRequests)
            {
                _logger.LogInformation("RequestID: {RequestID}, UserID: {UserID}, Department: {Department}, CreatedDate: {CreatedDate}, TotalPrice: {TotalPrice}",
                    request.RequestID, request.UserID, request.User?.Department, request.CreatedDate, request.TotalPrice);
            }

            // Pair each request to the department of the user who made the request and sum the cost
            var departmentCosts = filteredRequests
                .GroupBy(r => r.User?.Department ?? "Unknown")
                .Select(g => new DepartmentCostDto
                {
                    Department = g.Key,
                    Cost = g.Sum(r => r.TotalPrice)
                })
                .ToList();

            // Ensure all departments are included in the result, even if they have no requests
            foreach (var department in departments)
            {
                if (!departmentCosts.Any(dc => dc.Department == department))
                {
                    departmentCosts.Add(new DepartmentCostDto
                    {
                        Department = department,
                        Cost = 0
                    });
                }
            }

            return departmentCosts;
        }

        public async Task<List<SummaryDto>> GetAll()
        {
            var summaries = await _summaryRepository.GetAllInclude(s => s.Requests);
            return _mapper.Map < List<SummaryDto>>(summaries);

        }
        public async Task<Dictionary<int, List<RequestDto>>> GetApprovedSummariesWithRequests()
        {
            var approvedSummaries = await GetApprovedSummariesAsync();
            var result = new Dictionary<int, List<RequestDto>>();

            foreach (var summary in approvedSummaries)
            {
                var requests = await GetRequestsBySummaryId(summary.SummaryID);
                result.Add(summary.SummaryID, requests);
            }

            return result;
        }

        private async Task<List<Summary>> GetApprovedSummariesAsync()
        {
            var summaries = await _summaryRepository.GetAllAsync();
            return summaries.Where(s => s.IsApprovedBySupLead).ToList();
        }
        


    }
}
