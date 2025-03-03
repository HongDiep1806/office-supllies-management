using Office_supplies_management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task<bool> UpdateAsync(int id,T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> AddRanges (List<T> ranges);  
}