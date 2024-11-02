using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IBookRepository : ICrudRepository<Book>, IGetActive<Book>, IGetByName<Book>, ISoftDelete
    {
        Task<IEnumerable<Book>> GetBooksByCategoryId(int categoryid);
    }
}
