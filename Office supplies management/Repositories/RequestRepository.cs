using Office_supplies_management.DAL;
using Office_supplies_management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Repositories
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(Context context) : base(context)
        {
        }


    }
}

