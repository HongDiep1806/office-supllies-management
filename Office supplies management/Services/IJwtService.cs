namespace Office_supplies_management.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(int userID);
    }
}
