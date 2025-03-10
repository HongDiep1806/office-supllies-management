using Office_supplies_management.DTOs.Request;

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
    Task<List<RequestDto>> GetApprovedRequestsByDepLeader();
    Task<bool> ApproveRequestDepLeader(int requestId, int userId);
    Task<bool> ApproveRequestSupLead(int requestId, int userId);
    Task<bool> ApproveRequestSupLead(ApproveRequestSupLeadCommand command);
    Task<List<RequestDto>> GetAllRequestsForSupLeader(); // Add this method
}
