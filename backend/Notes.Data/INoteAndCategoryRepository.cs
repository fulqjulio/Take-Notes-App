using System.Linq.Expressions;
using Notes.Data.Models;

namespace Notes.Data.IRepository
{

    public interface INoteAndCategoryRepository
    {
        Task AddAsync(NoteAndCategory noteAndCategory);
        Task<NoteAndCategory> FindAsync(int id);
        Task<IEnumerable<NoteAndCategory>> GetAllAsync(
            Expression<Func<NoteAndCategory, bool>> filter =  null,
            Func<IQueryable<NoteAndCategory>, IOrderedQueryable<NoteAndCategory>> orderBy = null,
            string includeProperties = "");
        Task Update(NoteAndCategory noteAndCategory);
        Task Delete(NoteAndCategory noteAndCategory);
        Task Delete(int id);
        Task DeleteByNoteAndCategoryAsync(int noteId, int categoryId);
        Task DeleteByNoteOrCategoryAsync(int noteId, int categoryId);
        Task SaveAsync();
    }
}