using Microsoft.EntityFrameworkCore;
using Office_supplies_management.DAL;
using Office_supplies_management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly Context _context;

    public BaseRepository(Context context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            Console.WriteLine("tao thanh cong");
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());   
        }
        
    }

    public async Task<bool> UpdateAsync(int id, T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(id);
        if (existingEntity == null)
        {
            return false;
        }

        _context.Entry(existingEntity).CurrentValues.SetValues(entity); 
        await _context.SaveChangesAsync();

        return true;
    }



    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> AddRanges(List<T> ranges)
    {
        await _context.AddRangeAsync(ranges);
        await _context.SaveChangesAsync();
        return true;
    }
}