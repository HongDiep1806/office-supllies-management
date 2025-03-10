using Office_supplies_management.DTOs.Request;

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
    }
}
