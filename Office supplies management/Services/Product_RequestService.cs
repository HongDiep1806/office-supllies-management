using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.ProductRequest;
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

        public async Task<bool> Delete(int product_requestID)
        {
            return await _productRequestRepository.DeleteAsync(product_requestID);
        }

        public async Task<bool> DeleteForever(int requestID)
        {
            return await _productRequestRepository.DeleteForever(requestID);
        }

        public async Task<List<ProductRequestDto>> GetByRequestID(int requestID)
        {
            var productrequests = await _productRequestRepository.GetAllAsync();
            var productsOfRequest = productrequests.Where(pr => pr.RequestID == requestID).Select(pr => new ProductRequestDto
            {
                Product_RequestID = pr.Product_RequestID,
                ProductID = pr.ProductID,
                Quantity = pr.Quantity,
            }).ToList();
            return productsOfRequest;
        }
    }
}
