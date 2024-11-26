using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class BookCoverTypeRepository : IBookCoverTypeRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<BookCoverType> _bookCoverTypes;
        public BookCoverTypeRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _bookCoverTypes = _bookStoreContext.BookCoverType;
        }
        public async Task<object> AddAsync(BookCoverType entity)
        {
            return await _bookCoverTypes.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _bookCoverTypes.FindAsync(id);
            if (item != null) _bookCoverTypes.Remove(item);
        }

        public async Task<IEnumerable<BookCoverType>> GetAllAsync()
        {
            return await _bookCoverTypes.ToListAsync();
        }

        public async Task<BookCoverType> GetByIdAsync(int id)
        {
            return await _bookCoverTypes.FindAsync(id);
        }

        public void Update(BookCoverType entity)
        {
            _bookCoverTypes.Update(entity);
        }
        public async Task<IEnumerable<BookCoverType>> GetByNameAsync(string name)
        {
            return await _bookCoverTypes.Where(x => x.Name == name).ToListAsync();
        }
    }
}
