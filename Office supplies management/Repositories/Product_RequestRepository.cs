using Office_supplies_management.DAL;
using Office_supplies_management.Models;

namespace Office_supplies_management.Repositories
{
    public class Product_RequestRepository : BaseRepository<Product_Request>, IProduct_RequestRepository
    {
        public Product_RequestRepository(Context context) : base(context)
        {
        }
    }
}
