using Office_supplies_management.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Repositories
{
    public interface IRequestRepository : IBaseRepository<Request>
    {
        //Task<List<Request>> GetRequestsByDepartmentAsync(string departmentName, CancellationToken cancellationToken);
    }
}

