using Office_supplies_management.DTOs.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office_supplies_management.Services
{
    public interface IProductService
    {
        Task<List<ProductRequestDto>> GetAll();
        Task<ProductRequestDto> GetById(int id);
        Task<bool> Create(CreateProductDto request);
        Task<bool> Delete(int productId);
        Task<bool> Update(UpdateProductDto request);
    }
}
