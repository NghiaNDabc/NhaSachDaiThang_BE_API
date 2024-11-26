using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IBookCoverTypeRepository
    {
        Task<object> AddAsync(BookCoverType entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<BookCoverType>> GetAllAsync();
        Task<BookCoverType> GetByIdAsync(int id);
        Task<IEnumerable<BookCoverType>> GetByNameAsync(string name);

        void Update(BookCoverType entity);
    }
}
