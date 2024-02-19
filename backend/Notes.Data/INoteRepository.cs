using System.Linq.Expressions;
using Notes.Data.Models;

namespace Notes.Data.IRepository
{

    public interface INoteRepository
    {
        Task AddAsync(Note note);
        Task<Note> FindAsync(int id);
        Task<IEnumerable<Note>> GetAllAsync(
            Expression<Func<Note, bool>> filter =  null,
            Func<IQueryable<Note>, IOrderedQueryable<Note>> orderBy = null,
            string includeProperties = "");
        Task Update(Note note);
        Task Delete(Note note);
        Task Delete(int id);
        Task SaveAsync();
    }
}