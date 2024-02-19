using System.Linq.Expressions;
using Notes.Data.Models;

namespace Notes.Data.IRepository
{

    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category> FindAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync(
            Expression<Func<Category, bool>> filter =  null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            string includeProperties = "");
        Task Update(Category category);
        Task Delete(Category category);
        Task Delete(int id);
        Task SaveAsync();
    }
}