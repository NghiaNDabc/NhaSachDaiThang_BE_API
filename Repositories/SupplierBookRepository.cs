using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class SupplierBookRepository : ISupplierBookRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<SupplierBook> _supplierBooks;
        public SupplierBookRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _supplierBooks = _bookStoreContext.SupplierBook;
        }
        public async Task AddAsync(SupplierBook entity)
        {
            await _supplierBooks.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        { var item = await _supplierBooks.FindAsync(id);
            if (item != null)
            {
                _supplierBooks.Remove(item);
            }
        }
        public async Task<IEnumerable<SupplierBook>> GetByIdAsync(int? supplierid=null, int? bookid=null)
        {
            IQueryable<SupplierBook> query = _supplierBooks;
            if (supplierid != null)
            {
                query = query.Where(x=>x.SupplierId == supplierid);
            }
            if (bookid != null)
            {
                query = query.Where(x => x.BookId == bookid);
            }

            return await query.ToListAsync() ;
        }
        public async Task<IEnumerable<SupplierBook>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query = _supplierBooks;
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<SupplierBook>> GetByNameAsync(string bookName = null, string supplierName = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<SupplierBook> query = _supplierBooks;

            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(x => x.Book.Title.Contains(bookName));
            }

            if (!string.IsNullOrEmpty(supplierName))
            {
                query = query.Where(x => x.Supplier.Name.Contains(supplierName));
            }

            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }


        public async Task UpdateAsync(SupplierBook entity)
        {
            if( await _supplierBooks.FirstOrDefaultAsync(x=> x.SupplierId == entity.SupplierId && x.BookId==entity.BookId) != null)
            {
                _supplierBooks.Update(entity);
            }
        }
    }
}
