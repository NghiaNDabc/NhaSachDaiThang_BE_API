using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ISupplierBookRepository SupplierBookRepository { get; }

        Task<int> SaveChangeAsync();
    }
}
