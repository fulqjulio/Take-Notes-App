using Microsoft.AspNetCore.Mvc;
using Notes.Data.Models;
using Notes.Data.Dtos;
using Notes.Services.IServices;

namespace Notes.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAllCategoriesAsync")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            IEnumerable<Category> categories = [];
            try
            {
                categories = await _categoryService.GetCategoriesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return categories.Any() ? Ok(categories) : StatusCode(StatusCodes.Status404NotFound, "There is no any Category");
        }

        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById (int Id)
        {
            //NoteWithCategories Category;
            Category category = null;

            try
            {
                category = await _categoryService.GetCategoryByIdAsync(Id);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return category != null ? Ok(category) : StatusCode(StatusCodes.Status404NotFound, "This id does not exist for a Category");
        }

        [HttpPost]
        [Route("CreateCategoryAsync")]
        public async Task<IActionResult> CreateCategoryAsync(Category category)
        {
            Category result;
            try
            {
                result = await _categoryService.CreateCategoryAsync(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("UpdateCategoryAsync")]
        public async Task<IActionResult> UpdateCategoryAsync(Category category)
        {
            Category result;
            try
            {
                result = await _categoryService.UpdateCategoryAsync(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCategoryAsync")]
        public async Task<IActionResult> DeleteCategoryAsync(Category category)
        {
            Category result;
            try
            {
                result = await _categoryService.DeleteCategoryAsync(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCategoryByIdAsync")]
        public async Task<IActionResult> DeleteNDeleteCategoryByIdAsyncoteByIdAsync(int Id)
        {
            Category result;
            try
            {
                result = await _categoryService.DeleteCategoryByIdAsync(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }
    }
}