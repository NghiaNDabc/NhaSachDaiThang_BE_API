using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;
using System.Linq.Expressions;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category entity);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<IEnumerable<Category>> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        Task<IEnumerable<Category>> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null);
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category entity);
        Task<IEnumerable<CategoryDto>> GetCategoriesByLevel(int? parentId = null);
        Task<IEnumerable<CategoryDto>> GetCategoriesByLevelActive(int? parentId = null);
        Task<IEnumerable<Category>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null);
        Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate);
        Task<Category> GetActiveByIdAsync(int id);
        Task<IEnumerable<Category>> GetActiveByNameAsync(string name, int? pageNumber = null, int? pageSize = null);

    }
}
