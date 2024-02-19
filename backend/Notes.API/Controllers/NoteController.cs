using Microsoft.AspNetCore.Mvc;
using Notes.Data.Models;
using Notes.Data.Dtos;
using Notes.Services.IServices;

namespace Notes.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [Route("GetAllNotesAsync")]
        public async Task<IActionResult> GetAllNotesAsync()
        {
            IEnumerable<Note> notes = [];
            try
            {
                notes = await _noteService.GetNotesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return notes.Any() ? Ok(notes) : StatusCode(StatusCodes.Status404NotFound, "There is no any note");
        }

        [HttpGet]
        [Route("GetNoteById")]
        public async Task<IActionResult> GetNoteById (int Id)
        {
            //NoteWithCategories note;
            Note note = null;

            try
            {
                note = await _noteService.GetNoteByIdAsync(Id);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return note != null ? Ok(note) : StatusCode(StatusCodes.Status404NotFound, "This id does not exist for a note");
        }

        [HttpPost]
        [Route("CreateNoteAsync")]
        public async Task<IActionResult> CreateNoteAsync(Note note)
        {
            Note result;
            try
            {
                result = await _noteService.CreateNoteAsync(note);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("UpdateNoteAsync")]
        public async Task<IActionResult> UpdateNoteAsync(Note note)
        {
            Note result;
            try
            {
                result = await _noteService.UpdateNoteAsync(note);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteNoteAsync")]
        public async Task<IActionResult> DeleteNoteAsync(Note note)
        {
            Note result;
            try
            {
                result = await _noteService.DeleteNoteAsync(note);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteNoteByIdAsync")]
        public async Task<IActionResult> DeleteNoteByIdAsync(int Id)
        {
            Note result;
            try
            {
                result = await _noteService.DeleteNoteByIdAsync(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }
    }
}