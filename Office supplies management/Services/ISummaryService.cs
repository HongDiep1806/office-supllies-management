using Office_supplies_management.DTOs.Summary;

namespace Office_supplies_management.Services
{
    public interface ISummaryService
    {
        Task<SummaryDto> CreateSummary(CreateSummaryDto createSummaryDto);
    }
}
