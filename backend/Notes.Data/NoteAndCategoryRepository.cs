using System.Linq.Expressions;
using Notes.Data.IRepository;
using Notes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Data.Repository;

public class NoteAndCategoryRepository : INoteAndCategoryRepository
{
    internal NotesDBContext _context;
    internal DbSet<NoteAndCategory> _dbSet;

    public NoteAndCategoryRepository(NotesDBContext context)
    {
        _context = context;
        _dbSet = context.Set<NoteAndCategory>();
    }

    public async Task<NoteAndCategory> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<NoteAndCategory>> GetAllAsync(
        Expression<Func<NoteAndCategory, bool>> filter = null, 
        Func<IQueryable<NoteAndCategory>, IOrderedQueryable<NoteAndCategory>> orderBy = null, 
        string includeProperties = "")
    {
        IQueryable<NoteAndCategory> query = _dbSet;
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

    public async Task AddAsync(NoteAndCategory noteAndCategory)
    {
        await _dbSet.AddAsync(noteAndCategory);
    }

    public async Task Delete(NoteAndCategory noteAndCategory)
    {
        if(_context.Entry(noteAndCategory).State == EntityState.Detached)
        {
            _dbSet.Attach(noteAndCategory);                
        }
        _dbSet.Remove(noteAndCategory);
    }

    public async Task Delete(int id)
    {
        NoteAndCategory entitToDetelete = await _dbSet.FindAsync(id);
        Delete(entitToDetelete);
    }

    public async Task DeleteByNoteAndCategoryAsync(int noteId, int categoryId)
    {
        var entitiesToDelete = await _dbSet
            .Where(nc => nc.NoteId == noteId && nc.CategoryId == categoryId)
            .ToListAsync();

        if (entitiesToDelete.Any())
        {
            _dbSet.RemoveRange(entitiesToDelete);
        }
    }
    public async Task DeleteByNoteOrCategoryAsync(int noteId, int categoryId)
    {
        var entitiesToDelete = await _dbSet
            .Where(nc => nc.NoteId == noteId || nc.CategoryId == categoryId)
            .ToListAsync();

        if (entitiesToDelete.Any())
        {
            _dbSet.RemoveRange(entitiesToDelete);
        }
    }
    public async Task Update(NoteAndCategory noteAndCategory)
    {
        _dbSet.Attach(noteAndCategory);
        _context.Entry(noteAndCategory).State = EntityState.Modified;
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