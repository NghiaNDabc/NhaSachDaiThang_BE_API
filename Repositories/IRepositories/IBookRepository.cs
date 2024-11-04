using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IBookRepository //: ICrudRepository<Book>, IGetActive<Book>, IGetByName<Book>, ISoftDelete
    {
        Task AddAsync(Book entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null);
        Task<Book> GetActiveByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        Task<Book> GetByIdAsync(int id);
        Task SoftDelete(int id);
        Task UpdateAsync(Book entity);
        Task<IEnumerable<Book>> GetByNameAndCategoryIdAsync(int? categoryid = null, string? name = null, int? pageNumber = null, int? pageSize = null);
        Task<IEnumerable<Book>> GetActiveByNameAndCategoryIdAsync(int? categoryid = null, string? name = null, int? pageNumber = null, int? pageSize = null);
    }
}
