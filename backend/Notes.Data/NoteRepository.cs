using System.Linq.Expressions;
using Notes.Data.IRepository;
using Notes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Data.Repository;

public class NoteRepository : INoteRepository
{
    internal NotesDBContext _context;
    internal DbSet<Note> _dbSet;

    public NoteRepository(NotesDBContext context)
    {
        _context = context;
        _dbSet = context.Set<Note>();
    }

    public virtual async Task<Note> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<Note>> GetAllAsync(
        Expression<Func<Note, bool>> filter = null, 
        Func<IQueryable<Note>, IOrderedQueryable<Note>> orderBy = null, 
        string includeProperties = "")
    {
        IQueryable<Note> query = _dbSet;
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

    public virtual async Task AddAsync(Note note)
    {
        await _dbSet.AddAsync(note);
    }

    public virtual async Task Delete(Note note)
    {
        if(_context.Entry(note).State == EntityState.Detached)
        {
            _dbSet.Attach(note);                
        }
        _dbSet.Remove(note);
    }

    public virtual async Task Delete(int id)
    {
        Note entitToDetelete = await _dbSet.FindAsync(id);
        Delete(entitToDetelete);
    }

    public virtual async Task Update(Note note)
    {
        _dbSet.Attach(note);
        _context.Entry(note).State = EntityState.Modified;
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