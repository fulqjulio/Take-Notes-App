using Notes.Data.IRepository;
using Notes.Data.Models;
using Notes.Data.Repository;
using Notes.Services.IServices;

namespace Notes.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INoteAndCategoryRepository _noteAndCategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, INoteAndCategoryRepository noteAndCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _noteAndCategoryRepository = noteAndCategoryRepository;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            Category newCategory = new Category
            {
                Name = category.Name,
            };

            await _categoryRepository.AddAsync(newCategory);
            await _categoryRepository.SaveAsync();
            return newCategory;
        }

        public async Task<Category> DeleteCategoryAsync(Category category)
        {
            await _categoryRepository.Delete(category);
            await _noteAndCategoryRepository.DeleteByNoteOrCategoryAsync(0, category.Id);
            await _categoryRepository.SaveAsync();

            return category;
        }

        public async Task<Category> DeleteCategoryByIdAsync(int Id)
        {
            Category category = await _categoryRepository.FindAsync(Id);
            await _categoryRepository.Delete(category);
            await _noteAndCategoryRepository.DeleteByNoteOrCategoryAsync(0, Id);
            await _categoryRepository.SaveAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return _categoryRepository.FindAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
            return category;
        }
    }
}
