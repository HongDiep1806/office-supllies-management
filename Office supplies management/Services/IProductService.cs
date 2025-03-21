using Office_supplies_management.DTOs.Product;

public interface IProductService
{
    Task<List<ProductDto>> GetAll();
    Task<List<ProductDto>> AllItems();
    Task<ProductDto> GetById(int id);
    Task<ProductDto> Create(CreateProductDto request);
    Task<bool> Delete(int productId);
    Task<bool> Update(UpdateProductDto request);
    Task<List<ProductDto>> SearchProductsAsync(string name, string code, decimal? minPrice, decimal? maxPrice);
    Task<ProductDto> GetProductByCodeAsync(string code);
}