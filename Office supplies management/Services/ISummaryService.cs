using Office_supplies_management.DTOs.Request;
using Office_supplies_management.DTOs.Summary;

public interface ISummaryService
{
    Task<SummaryDto> CreateSummary(CreateSummaryDto createSummaryDto);
    Task<bool> UpdateSummary(UpdateSummaryDto updateSummaryDto);
    Task<List<SummaryDto>> GetAllSummaries();
    Task<List<SummaryDto>> GetSummariesByUserId(int userId);
    Task<SummaryDto> GetSummaryById(int summaryId);
    Task<List<DepartmentUsageReportDto>> GetDepartmentUsageReport(string department, DateTime startDate, DateTime endDate);
    Task<List<SummaryDto>> GetSummariesByDateRange(DateTime startDate, DateTime endDate);
}