using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Services
{
    public class Product_RequestService : IProduct_RequestService
    {
        private readonly IProduct_RequestRepository _productRequestRepository;
        public Product_RequestService(IProduct_RequestRepository productRequestRepository)
        {
            _productRequestRepository = productRequestRepository; 
        }
        public async Task<bool> AddRanges(List<Product_Request> product_requests)
        {
            await _productRequestRepository.AddRanges(product_requests);
            return true;
        }
    }
}
