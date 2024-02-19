using Notes.Data.Dtos;
using Notes.Data.Models;

namespace Notes.Services.IServices
{
    public interface INoteWithCategoryService
    {
        Task<IEnumerable<NoteWithCategories>> GetNotesWithCategoriesAsync();

        Task<NoteWithCategories> GetNoteWithCategoryByIdAsync(int Id);
        Task<IEnumerable<NoteWithCategories>> GetNoteWithCategoryArchivedOrNotAsync(bool archived); 
        Task<IEnumerable<NoteWithCategories>> GetNoteByCategoryAsync(int categoryId);
        Task<NoteWithCategories> AddCategoryToNoteAsync(NoteAndCategory noteAndCategory); 
        Task<NoteWithCategories> UpdateCategoryToNoteAsync(NoteAndCategory noteAndCategory); 
        Task<NoteWithCategories> DeleteCategoryFromNoteAsync(NoteAndCategory noteAndCategory);
        Task<NoteWithCategories> DeleteNoteWithCategoryByIdAsync(int noteId, int categoryId);
    }
}