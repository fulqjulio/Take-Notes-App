using Microsoft.AspNetCore.Mvc;
using Notes.Data.Models;
using Notes.Data.Dtos;
using Notes.Services.IServices;

namespace Notes.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class NoteAndCategoryController : ControllerBase
    {
        private readonly INoteWithCategoryService _noteWithCategoryService;

        public NoteAndCategoryController(INoteWithCategoryService noteWithCategoryService)
        {
            _noteWithCategoryService = noteWithCategoryService;
        }

        [HttpGet]
        [Route("GetNotesWithCategories")]
        public async Task<IActionResult> GetNotesWithCategories()
        {
            IEnumerable<NoteWithCategories> notes = [];
            try
            {
                notes = await _noteWithCategoryService.GetNotesWithCategoriesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return notes.Any() ? Ok(notes) : StatusCode(StatusCodes.Status404NotFound, "There is no any note");
        }

        [HttpGet]
        [Route("GetNoteWithCategoryById")]
        public async Task<IActionResult> GetNoteWithCategoryById(int Id)
        {
            //NoteWithCategories note;
            NoteWithCategories note = null;

            try
            {
                note = await _noteWithCategoryService.GetNoteWithCategoryByIdAsync(Id);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return note != null ? Ok(note) : StatusCode(StatusCodes.Status404NotFound, "This id does not exist for a note");
        }

        [HttpGet]
        [Route("GetNoteWithCategoryArchived")]
        public async Task<IActionResult> GetNoteWithCategoryArchivedOrNot(bool archived)
        {
             IEnumerable<NoteWithCategories> notes = null;

            try
            {
                notes = await _noteWithCategoryService.GetNoteWithCategoryArchivedOrNotAsync(archived);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return notes.Any() ? Ok(notes) : StatusCode(StatusCodes.Status404NotFound, $"This is no archived: {archived}");
        }

        [HttpGet]
        [Route("GetNoteByCategory")]
        public async Task<IActionResult> GetNoteByCategory(int categoryId)
        {
            IEnumerable<NoteWithCategories> notes = null;

            try
            {
                notes = await _noteWithCategoryService.GetNoteByCategoryAsync(categoryId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return notes.Any() ? Ok(notes) : StatusCode(StatusCodes.Status404NotFound, $"This is no any note with the category: {categoryId}");
        }

        [HttpPost]
        [Route("AddCategoryToNote")]
        public async Task<IActionResult> AddCategoryToNote(NoteAndCategory noteAndCategory)
        {
            NoteWithCategories result;
            try
            {
                result = await _noteWithCategoryService.AddCategoryToNoteAsync(noteAndCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("UpdateCategoryToNote")]
        public async Task<IActionResult> UpdateCategoryToNote(NoteAndCategory noteAndCategory)
        {
            NoteWithCategories result;
            try
            {
                result = await _noteWithCategoryService.UpdateCategoryToNoteAsync(noteAndCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCategoryFromNote")]
        public async Task<IActionResult> DeleteCategoryFromNote(NoteAndCategory noteAndCategory)
        {
            NoteWithCategories result;
            try
            {
                result = await _noteWithCategoryService.DeleteCategoryFromNoteAsync(noteAndCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteNoteWithCategoryById")]
        public async Task<IActionResult> DeleteNoteWithCategoryById(int noteId, int categoryId)
        {
            NoteWithCategories result;
            try
            {
                result = await _noteWithCategoryService.DeleteNoteWithCategoryByIdAsync(noteId, categoryId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }
    }
}