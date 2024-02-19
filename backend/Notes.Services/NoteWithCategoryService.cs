using Notes.Data.Dtos;
using Notes.Data.IRepository;
using Notes.Data.Models;
using Notes.Services.IServices;
using System.Net;
using System.Xml;

namespace Notes.Services;

public class NoteWithCategoryService : INoteWithCategoryService
{
    private static INoteRepository _noteRepository;
    private static INoteAndCategoryRepository _noteAndCategoryRepository;
    private static ICategoryRepository _categoryRepository;

    public NoteWithCategoryService(INoteRepository noteRepository, INoteAndCategoryRepository noteAndCategoryRepository, ICategoryRepository categoryRepository)
    {
        _noteRepository = noteRepository;
        _noteAndCategoryRepository = noteAndCategoryRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<NoteWithCategories> AddCategoryToNoteAsync(NoteAndCategory noteAndCategory)
    {
        IEnumerable<NoteAndCategory> exist = await _noteAndCategoryRepository.GetAllAsync(x => x.NoteId == noteAndCategory.NoteId && x.CategoryId == noteAndCategory.CategoryId);

        if (exist.Any())
        {
            throw new Exception("The note already has the category");
        }

        NoteAndCategory newNoteAndCategory = new NoteAndCategory
        {
            NoteId = noteAndCategory.NoteId,
            CategoryId = noteAndCategory.CategoryId,
        };

        await _noteAndCategoryRepository.AddAsync(newNoteAndCategory);
        await _noteAndCategoryRepository.SaveAsync();

        return await GetNoteWithCategories(newNoteAndCategory.NoteId);
    }

    public async Task<IEnumerable<NoteWithCategories>> GetNoteWithCategoryArchivedOrNotAsync(bool archived)
    {
        List<NoteWithCategories> result = new List<NoteWithCategories>();
        IEnumerable<Note> notes = await _noteRepository.GetAllAsync(x => x.IsArchived == archived);

        foreach (var note in notes)
        {
            result.Add(await GetNoteWithCategoriesEntity(note));
        }

        return result;
    }
    public async Task<IEnumerable<NoteWithCategories>> GetNoteByCategoryAsync(int categoryId)
    {
        IEnumerable<NoteAndCategory> notesAndCategories = await _noteAndCategoryRepository.GetAllAsync(x => x.CategoryId == categoryId);
        IEnumerable<int> notesIds = notesAndCategories.Select(x => x.CategoryId).ToList();

        List<NoteWithCategories> result = new List<NoteWithCategories>();

        foreach (var noteId in notesIds)
        {
            result.Add(await GetNoteWithCategories(noteId));
        }

        return result;
    }
    public async Task<NoteWithCategories> DeleteCategoryFromNoteAsync(NoteAndCategory noteAndCategory)
    {
        await _noteAndCategoryRepository.DeleteByNoteAndCategoryAsync(noteAndCategory.NoteId, noteAndCategory.CategoryId);
        await _noteAndCategoryRepository.SaveAsync();
        return await GetNoteWithCategories(noteAndCategory.NoteId);
    }

    public async Task<NoteWithCategories> DeleteNoteWithCategoryByIdAsync(int noteId, int categoryId)
    {
        await _noteAndCategoryRepository.DeleteByNoteAndCategoryAsync(noteId, categoryId);
        await _noteAndCategoryRepository.SaveAsync();
        return await GetNoteWithCategories(noteId);
    }

    public async Task<NoteWithCategories> GetNoteWithCategoryByIdAsync(int id)
    {
        return await GetNoteWithCategories(id);
    }

    public async Task<IEnumerable<NoteWithCategories>> GetNotesWithCategoriesAsync()
    {
        List<NoteWithCategories> result = new List<NoteWithCategories>();
        IEnumerable<Note>notes = await _noteRepository.GetAllAsync();

        foreach (var note in notes)
        {
            result.Add(await GetNoteWithCategoriesEntity(note));
        }

        return result;
    }

    public async Task<NoteWithCategories> UpdateCategoryToNoteAsync(NoteAndCategory noteAndCategory)
    {
        await _noteAndCategoryRepository.Update(noteAndCategory);
        await _noteAndCategoryRepository.SaveAsync();

        Note note = await _noteRepository.FindAsync(noteAndCategory.NoteId);
        note.LastUpdatedTime = DateTime.Now;
        await _noteRepository.Update(note);
        await _noteRepository.SaveAsync();
        return await GetNoteWithCategories(note.Id);
    }

    public static async Task<NoteWithCategories> GetNoteWithCategories(int noteId)
    {
        Note note = await _noteRepository.FindAsync(noteId);
        IEnumerable<NoteAndCategory> notesAndCategories = await _noteAndCategoryRepository.GetAllAsync(x => x.NoteId == noteId);
        IEnumerable<int> categoriesIds = notesAndCategories.Select(x => x.CategoryId).ToList();

        NoteWithCategories newNoteWithCategories = new NoteWithCategories
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            IsArchived = note.IsArchived,
            CreatedTime = note.CreatedTime,
            LastUpdatedTime = note.LastUpdatedTime,
            Categories = await _categoryRepository.GetAllAsync(x => categoriesIds.Contains(x.Id))
        };

        return newNoteWithCategories;
    }

    public static async Task<NoteWithCategories> GetNoteWithCategoriesEntity(Note note)
    {
        IEnumerable<NoteAndCategory> notesAndCategories = await _noteAndCategoryRepository.GetAllAsync(x => x.NoteId == note.Id);
        IEnumerable<int> categoriesIds = notesAndCategories.Select(x => x.CategoryId).ToList();

        NoteWithCategories newNoteWithCategories = new NoteWithCategories
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            IsArchived = note.IsArchived,
            CreatedTime = note.CreatedTime,
            LastUpdatedTime = note.LastUpdatedTime,
            Categories = await _categoryRepository.GetAllAsync(x => categoriesIds.Contains(x.Id))
        };

        return newNoteWithCategories;
    }

}
