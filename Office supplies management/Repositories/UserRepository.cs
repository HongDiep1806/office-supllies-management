using Microsoft.EntityFrameworkCore;
using Office_supplies_management.DAL;
using Office_supplies_management.Models;

namespace Office_supplies_management.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public async Task<string> GetNameById(int id)
        {
            var fullName = await _context.Users
                .AsNoTracking()
                .Where(u => u.UserID == id)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            return fullName ?? "Không tìm thấy người dùng";
        }
    }
}
