using Notes.Data.IRepository;
using Notes.Data.Models;
using Notes.Services.IServices;
using System.Net;

namespace Notes.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly INoteAndCategoryRepository _noteAndCategoryRepository;

    public NoteService(INoteRepository noteRepository, INoteAndCategoryRepository noteAndCategoryRepository)
    {
        _noteRepository = noteRepository;
        _noteAndCategoryRepository = noteAndCategoryRepository;
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        Note newNote = new Note
        {
            Title = note.Title,
            Content = note.Content,
            IsArchived = note.IsArchived,
            CreatedTime = DateTime.Now,
            LastUpdatedTime = DateTime.Now
        };

        await _noteRepository.AddAsync(newNote);
        await _noteRepository.SaveAsync();
        return newNote;
    }

    public async Task<Note> DeleteNoteAsync(Note note)
    {
        await _noteRepository.Delete(note);
        await _noteAndCategoryRepository.DeleteByNoteAndCategoryAsync(note.Id, 0);
        await _noteRepository.SaveAsync();
        return note;
    }

    public async Task<Note> DeleteNoteByIdAsync(int Id)
    {
        Note note = await _noteRepository.FindAsync(Id);
        await _noteRepository.Delete(note);
        await _noteAndCategoryRepository.DeleteByNoteOrCategoryAsync(Id, 0);
        await _noteRepository.SaveAsync();
        return note;
    }

    public async Task<Note> GetNoteByIdAsync(int id)
    {
        return await _noteRepository.FindAsync(id);
    }

    public async Task<IEnumerable<Note>> GetNotesAsync()
    {
        return await _noteRepository.GetAllAsync();
    }

    public async Task<Note> UpdateNoteAsync(Note note)
    {
        note.LastUpdatedTime = DateTime.Now;
        await _noteRepository.Update(note);
        await _noteRepository.SaveAsync();
        return note;
    }

}
