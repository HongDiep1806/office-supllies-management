using Office_supplies_management.DAL;
using Office_supplies_management.Models;

namespace Office_supplies_management.Repositories
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(Context context) : base(context)
        {
        }
    }
}
