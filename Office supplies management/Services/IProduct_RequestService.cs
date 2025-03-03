using Office_supplies_management.Models;

namespace Office_supplies_management.Services
{
    public interface IProduct_RequestService
    {
        Task<bool> AddRanges(List<Product_Request> product_requests);
    }
}
