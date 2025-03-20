using AutoMapper;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office_supplies_management.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(CreateProductDto createProductDto)
        {
            var newProduct = new Product
            {
                Name = createProductDto.Name,
                Code = createProductDto.Code,
                UnitCurrency = createProductDto.UnitCurrency,
                UnitPrice = createProductDto.UnitPrice,
                UserIDCreate = createProductDto.UserIDCreate,
                CreateDate = DateTime.Now,
                UserIDAdjust = createProductDto.UserIDAdjust,
                AdjustDate = DateTime.Now
            };
            await _productRepository.CreateAsync(newProduct);
            return _mapper.Map<ProductDto>(newProduct);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> Update(UpdateProductDto updateProductDto)
        {
            var currentProduct = await _productRepository.GetByIdAsync(updateProductDto.ProductID);
            var updatedProduct = _mapper.Map<Product>(updateProductDto);
            updatedProduct.UserIDCreate = currentProduct.UserIDCreate; // Preserve existing value
            updatedProduct.CreateDate = currentProduct.CreateDate; // Preserve existing value
            updatedProduct.UserIDAdjust = updateProductDto.UserIDAdjust; // Set new value
            updatedProduct.AdjustDate = DateTime.Now; // Set new value
            return await _productRepository.UpdateAsync(updateProductDto.ProductID, updatedProduct);
        }

        public async Task<bool> Delete(int productId)
        {
            return await _productRepository.DeleteAsync(productId);
        }

        public async Task<bool> GetByCode(string code)
        {
            var products = await GetAll();
            var product = products.FirstOrDefault(p => p.Code == code);
            if (product != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ProductDto>> AllItems()
        {
            var products = await _productRepository.AllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
        public async Task<List<ProductDto>> SearchProductsAsync(string? name, string? code, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(code))
            {
                products = products.Where(p => p.Code.Contains(code, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => decimal.Parse(p.UnitPrice) >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => decimal.Parse(p.UnitPrice) <= maxPrice.Value).ToList();
            }

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
