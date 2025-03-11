using Office_supplies_management.DAL;
using Office_supplies_management.Models;

namespace Office_supplies_management.Repositories
{
    public class SummaryRepository : BaseRepository<Summary>, ISummaryRepository
    {
        public SummaryRepository(Context context) : base(context)
        {
        }
    }
}
