using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
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
            var book = await _books.FirstOrDefaultAsync(book=>book.BookId ==id);
            if (book != null) 
             _books.Remove(book);
        }

        public async Task<IEnumerable<Book>> GetAllActiveAsync()
        {
            return await _books.Where(b => b.IsDel == false).ToListAsync();
        }

        public async Task<Book> GetActiveByIdAsync(int id)
        {
            return await _books.Where(b => b.BookId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryId(int id)
        {
            return await _books.Where(b=> b.CategoryId == id).ToListAsync();    
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var rs =  await _books.FirstOrDefaultAsync(book=>book.BookId==id);
            return rs;
        }

        public async Task<IEnumerable<Book>> GetByNameAsync(string name)
        {
            return await _books.Where(b => b.Title.Contains(name)).ToListAsync();
        }

        public async Task SoftDelete(int id)
        {
            var book = await _books.FirstOrDefaultAsync(x=>x.BookId == id);
            if (book != null)
            {
                book.IsDel = true;
               await UpdateAsync(book);
            }
            
        }

        public async Task UpdateAsync(Book entity)
        {
            if( await _books.AnyAsync(b=>b.BookId == entity.BookId))
                _books.Update(entity);
        }

        public async Task<IEnumerable<Book>> GetActiveByNameAsync(string name)
        {
            return await _books.Where(b => b.Title.Contains(name) && b.IsDel == false).ToListAsync();
        }
    }
}
