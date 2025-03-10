using Office_supplies_management.Repositories;

public class ApproveRequestSupLeadHandler
{
    private readonly IRequestRepository _requestRepository;

    public ApproveRequestSupLeadHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<bool> Handle(ApproveRequestSupLeadCommand command)
    {
        if (command.UserRole != "Finance Management Employee")
        {
            return false;
        }

        var requestEntity = await _requestRepository.GetByIdAsync(command.RequestId);
        if (requestEntity == null || requestEntity.UserID != command.UserId)
        {
            return false;
        }

        requestEntity.IsApprovedBySupLead = true;
        return await _requestRepository.UpdateAsync(command.RequestId, requestEntity);
    }
}
