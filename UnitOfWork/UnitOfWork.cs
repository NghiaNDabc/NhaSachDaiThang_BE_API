using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Repositories;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext _bookStoreContext;
        public IUserRepository UserRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            UserRepository = new UserRepository(_bookStoreContext);
            CategoryRepository = new CategoryRepository(_bookStoreContext);
        }

        public void Dispose()
        {
            _bookStoreContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _bookStoreContext.SaveChangesAsync();
        }
    }
}
