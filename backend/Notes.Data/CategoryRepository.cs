using System.Linq.Expressions;
using Notes.Data.IRepository;
using Notes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Data.Repository;

public class CategoryRepository: ICategoryRepository
{
    internal NotesDBContext _context;
    internal DbSet<Category> _dbSet;

    public CategoryRepository(NotesDBContext context)
    {
        _context = context;
        _dbSet = context.Set<Category>();
    }

    public virtual async Task<Category> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<Category>> GetAllAsync(
        Expression<Func<Category, bool>> filter = null, 
        Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, 
        string includeProperties = "")
    {
        IQueryable<Category> query = _dbSet;
        if(filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(
            new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if(orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task AddAsync(Category category)
    {
        await _dbSet.AddAsync(category);
    }

    public virtual async Task Delete(Category category)
    {
        if(_context.Entry(category).State == EntityState.Detached)
        {
            _dbSet.Attach(category);                
        }
        _dbSet.Remove(category);
    }

    public virtual async Task Delete(int id)
    {
        Category entitToDetelete = await _dbSet.FindAsync(id);
        await Delete(entitToDetelete);
    }

    public virtual async Task Update(Category category)
    {
        _dbSet.Attach(category);
        _context.Entry(category).State = EntityState.Modified;
    }

    public async Task SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();
        }
    }
}