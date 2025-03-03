using Office_supplies_management.DAL;
using Office_supplies_management.Models;

namespace Office_supplies_management.Repositories
{
    public class PermissionRepsitory : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepsitory(Context context) : base(context)
        {
        }
    }
}
