using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.Models;

namespace Office_supplies_management.Services
{
    public interface IProduct_RequestService
    {
        Task<bool> AddRanges(List<Product_Request> product_requests);
        Task<List<ProductRequestDto>> GetByRequestID(int requestID);    
        Task<bool>Delete(int requestID);
        Task<bool> DeleteForever (int requestID);   
    }
}
