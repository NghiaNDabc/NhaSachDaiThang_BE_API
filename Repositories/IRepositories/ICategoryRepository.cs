using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ICategoryRepository : ICrudRepository<Category>, ISoftDelete, IGetActive<Category>, IGetByName<Category>, IAny<Category>
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesByLevel(int? parentId = null);
        Task<IEnumerable<CategoryDto>> GetCategoriesByLevelActive(int? parentId = null);
    }
}
