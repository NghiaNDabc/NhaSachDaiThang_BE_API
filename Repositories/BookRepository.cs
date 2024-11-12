using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly DbSet<Book> _books;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
            _books = _context.Book;
        }
        public async Task AddAsync(Book entity)
        {
            await _books.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _books.FirstOrDefaultAsync(book => book.BookId == id);
            if (book != null)
                _books.Remove(book);
        }
        public async Task<IEnumerable<Book>> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query = _books.Where(b => b.IsDel == false);
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public async Task<Book> GetActiveByIdAsync(int id)
        {
            return await _books.Where(b => b.BookId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query = _books;
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var rs = await _books.FirstOrDefaultAsync(book => book.BookId == id);
            return rs;
        }


        public async Task SoftDelete(int id)
        {
            var book = await _books.FirstOrDefaultAsync(x => x.BookId == id);
            if (book != null)
            {
                book.IsDel = !book.IsDel;
                await UpdateAsync(book);
            }

        }

        public async Task UpdateAsync(Book entity)
        {
            if (await _books.AnyAsync(b => b.BookId == entity.BookId))
                _books.Update(entity);
        }


        public async Task<IEnumerable<Book>> GetByNameAndCategoryIdAsync(int? categoryid = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Book> query = _books;
            if(name != null)
            {
                query = query.Where(b => b.Title.Contains(name));
            }
            if (categoryid.HasValue)
            {
                query = query.Where(b => b.CategoryId ==  categoryid);
            }
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }
        public async Task<IEnumerable<Book>> GetActiveByNameAndCategoryIdAsync(int? categoryid = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Book> query = _books.Where(b=>b.IsDel == false);
            if (name != null)
            {
                query = query.Where(b => b.Title.Contains(name));
            }
            if (categoryid.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryid);
            }
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public Task<int> CountActive()
        {
            return _books.CountAsync(x=> x.IsDel == false);
        }

        public Task<int> CountDeactive()
        {
            return _books.CountAsync(x => x.IsDel == true);
        }
    }
}
