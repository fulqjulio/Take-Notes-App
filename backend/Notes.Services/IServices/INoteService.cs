using Notes.Data.Models;

namespace Notes.Services.IServices
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetNotesAsync();
        Task<Note> GetNoteByIdAsync(int id);
        Task<Note> CreateNoteAsync(Note note);
        Task<Note> UpdateNoteAsync(Note note);
        Task<Note> DeleteNoteAsync(Note note);
        Task<Note> DeleteNoteByIdAsync(int Id);
    }
}