using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;

namespace Office_supplies_management.Services
{
    public interface IRequestService
    {
        Task<List<RequestDto>> GetByUserID(int userId);
        Task<RequestDto> Create(CreateRequestDto createRequest);
        Task<List<RequestDto>> GetAll();
        Task<RequestDto> GetByID(int id);
        Task<bool> Update(UpdateRequestDto updateRequest);
        Task<bool> DeleteByID(int id);
        Task<int> Count();
        Task<List<RequestDto>> GetByDepartment(string department);
        Task<bool> ApproveByDepLeader(int requestID);
        Task<List<RequestDto>> GetApprovedRequestsByDepLeader();
        Task<bool> ApproveByFinEmployee(int requestId);
        Task<List<RequestDto>> GetAllRequestsForFinEmployee();
        Task<bool> NotApproveRequestByDepLeader(int requestId);
        Task<bool> NotApproveRequestByFinEmployee(int requestId);
        Task UpdateRequestStatus(int summaryID, bool isProcessedBySupLead, bool isApprovedBySupLead);
        Task<List<RequestDto>> GetCollectedRequests(); // Add this method
        Task<List<RequestDto>> GetRequestsInApprovedSummary(); // New method
    }
}
