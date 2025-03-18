using Office_supplies_management.DTOs.Request;
using Office_supplies_management.DTOs.Summary;

namespace Office_supplies_management.Services
{
    public interface ISummaryService
    {
        Task<SummaryDto> CreateSummary(CreateSummaryDto createSummaryDto);
        Task<List<SummaryDto>> GetAll();
        Task<bool> UpdateSummary(UpdateSummaryDto updateSummaryDto);
        Task<List<SummaryDto>> GetAllSummaries();
        Task<List<SummaryDto>> GetSummariesByUserId(int userId);
        Task<SummaryDto> GetSummaryById(int summaryId);
        Task<List<DepartmentUsageReportDto>> GetDepartmentUsageReport(string department, DateTime startDate, DateTime endDate);
        Task<List<SummaryDto>> GetSummariesByDateRange(DateTime startDate, DateTime endDate);
        Task<List<RequestDto>> GetRequestsBySummaryId(int summaryId); // Add this method
        Task<List<DepartmentCostDto>> GetDepartmentCosts(DateTime startDate, DateTime endDate);
        Task<Dictionary<int, List<RequestDto>>> GetApprovedSummariesWithRequests();
        Task<List<RequestDto>> GetSummariesWithRequestsByDateRange(DateTime startDate, DateTime endDate);
        Task<int> CountSummaries();
    }
}
