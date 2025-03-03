using Office_supplies_management.DAL;
using Office_supplies_management.Models;

namespace Office_supplies_management.Repositories
{
    public class UserType_PermissionRepository : BaseRepository<UserType_Permission>, IUserType_PermissionRepository
    {
        public UserType_PermissionRepository(Context context) : base(context)
        {
        }
    }
}
