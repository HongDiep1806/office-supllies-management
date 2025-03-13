using Microsoft.EntityFrameworkCore;
using Office_supplies_management.DAL;
using Office_supplies_management.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        var allitems = await _context.Set<T>().ToListAsync();
        return allitems.Where(a => a.IsDeleted==false).ToList();
    }

    public async Task<int> Count()
    {
        var allitems = await _context.Set<T>().ToListAsync();
        return allitems.Count();
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
        await _context.Set<T>().AddRangeAsync(ranges);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<T>> AllAsync()
    {
        var allitems = await _context.Set<T>().ToListAsync();
        return allitems;
    }

    public async Task<bool> DeleteForever(int id)
    {
        var entity = await GetByIdAsync(id);
         _context.Set<T>().Remove(entity); 
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<T>> GetAllInclude(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }
}