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

        public async Task<bool> Create(CreateProductDto dto)
        {
            var existedProductCode = await GetByCode(dto.Code);
            if (!existedProductCode)
            {
                var newProduct = _mapper.Map<Product>(dto);
                await _productRepository.CreateAsync(newProduct);
                return true;
            }
            return false;

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

        public async Task<bool> Update(UpdateProductDto dto)
        {
            var updatedProduct = _mapper.Map<Product>(dto);
            return await _productRepository.UpdateAsync(dto.ProductID, updatedProduct);
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
    }
}
