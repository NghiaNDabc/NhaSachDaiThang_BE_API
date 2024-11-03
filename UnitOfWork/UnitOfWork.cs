using AutoMapper;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Repositories;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly IMapper _mapper;
        public IUserRepository UserRepository { get; }

        public ICategoryRepository CategoryRepository { get; }
        public IBookRepository BookRepository { get; }

        public ISupplierRepository SupplierRepository {  get; }
        public UnitOfWork(BookStoreContext bookStoreContext, IMapper mapper)
        {
            _mapper = mapper;
            _bookStoreContext = bookStoreContext;
            UserRepository = new UserRepository(_bookStoreContext);
            CategoryRepository = new CategoryRepository(_bookStoreContext, _mapper);
            BookRepository = new BookRepository(_bookStoreContext);
            SupplierRepository =  new SupplierRepository(_bookStoreContext);
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
