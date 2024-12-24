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
        { var item =  _supplierBooks.Where(s=>s.SupplierBookId==id);
            if (item != null)
            {
                _supplierBooks.RemoveRange(item);
            }
        }
        public async Task<IEnumerable<SupplierBook>> GetByIdAsync(int supplierId)
        {
            return await _supplierBooks.Where(x=>x.SupplierId== supplierId).ToListAsync();
        }
        public async Task<IEnumerable<SupplierBook>> GetAllAsync()
        {
            var query = _supplierBooks.OrderByDescending(x=>x.CreatedDate);
            return query;
        }

        public async Task<IEnumerable<SupplierBook>> GetByFilterAsync(int? bookId = null, int? supplierId = null, string? bookName = null, string? supplierName = null, DateTime? minDate = null, DateTime? maxDate = null)
        {
            IQueryable<SupplierBook> query = _supplierBooks;
            if (bookId.HasValue)
            {
                query = query.Where(x => x.BookId == bookId.Value);
            }
            if (supplierId.HasValue)
            {
                query = query.Where(x => x.SupplierId == supplierId.Value);
            }
            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(x => x.Book.Title.Contains(bookName));
            }

            if (!string.IsNullOrEmpty(supplierName))
            {
                query = query.Where(x => x.Supplier.Name.Contains(supplierName));
            }
            if (minDate.HasValue)
            {
                query = query.Where(x => x.SupplyDate.Value >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                query = query.Where(x => x.SupplyDate.Value <= maxDate.Value);
            }
            return query;
        }


        public async Task UpdateAsync(SupplierBook entity)
        {
            if( await _supplierBooks.FirstOrDefaultAsync(x=> x.SupplierId == entity.SupplierId && x.BookId==entity.BookId) != null)
            {
                _supplierBooks.Update(entity);
            }
        }

        public async Task<int> GetNextSupplierBookIdAsync()
        {
            var maxSupplierBookId = await _supplierBooks.MaxAsync(sb => (int?)sb.SupplierBookId) ?? 0;
            return maxSupplierBookId+1;

        }
    }
}
