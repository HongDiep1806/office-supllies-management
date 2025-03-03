using Office_supplies_management.DAL;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(Context context) : base(context)
    {
    }
}