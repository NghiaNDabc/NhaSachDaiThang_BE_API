using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }

        Task<int> SaveChangeAsync();
    }
}
