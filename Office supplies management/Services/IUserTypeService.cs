using Office_supplies_management.DTOs.Permission;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.UserType;

namespace Office_supplies_management.Services
{
    public interface IUserTypeService
    {
        //Task<List<ProductDto>> GetAll();
        Task<UserTypeDto> GetById(int id);
        Task<List<PermissionDto>> GetPermission(int usertypeId);  
        //Task<ProductDto> Create(CreateProductDto request);
        //Task<bool> Delete(int productId);
        //Task<bool> Update(UpdateProductDto request);
    }
}
