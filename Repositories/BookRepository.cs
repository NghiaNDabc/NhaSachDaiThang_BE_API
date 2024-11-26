using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using System.Net.WebSockets;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly DbSet<Book> _books;
        private readonly DbSet<Category> _categories;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
            _books = _context.Book;
            _categories = _context.Category;
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


        public async Task SoftDeleteAsync(int id)
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

        public async Task<int> CountByFillterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, int? languageId = null, int? bookCoverTypeId = null, bool? IsDel = null)
        {

            IQueryable<Book> query = _books;
            if (IsDel.HasValue)
            {
                query = query.Where(b => b.IsDel.Value == IsDel.Value);
            }
            if (bookName != null)
            {
                query = query.Where(b => b.Title.Contains(bookName));
            }
            
            var y = query.ToList();
            if (categoryid.HasValue)
            {
                List<int> listcategoryId = new List<int>();
                listcategoryId.Add(categoryid.Value);
                var subCategory = _categories.Where(b => b.ParentCategoryID.HasValue && b.ParentCategoryID.Value == categoryid.Value);

                if (subCategory != null)
                {
                    foreach (var category in subCategory)
                    {
                        listcategoryId.Add(category.CategoryId);
                    }
                }
                query = query.Where(b => listcategoryId.Contains(b.CategoryId.Value));
            }
            else if (!string.IsNullOrEmpty(categoryName))
            {
                List<int> listcategoryId = new List<int>();
                var parentCategory = _categories.Where(b => b.Name.Contains(categoryName));
                //listcategoryId.Add(categoryid.Value);
                //var subCategory = _categories.Where(b => b.ParentCategoryID == categoryid);

                if (parentCategory != null)
                {
                    foreach (var category in parentCategory)
                    {
                        listcategoryId.Add(category.CategoryId);
                        var subcategories = category.SubCategories;
                        if (subcategories != null && subcategories.Count() > 0)
                        {
                            foreach (var subcategory in subcategories)
                            {
                                listcategoryId.Add(subcategory.CategoryId);

                            }
                        }
                    }
                }
                query = query.Where(b => listcategoryId.Contains(b.CategoryId.Value));
            }
            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }
            if (minQuality.HasValue)
            {
                query = query.Where(b => b.Quantity >= minQuality.Value);
            }
            if (maxQuanlity.HasValue)
            {
                query = query.Where(b => b.Quantity <= maxQuanlity.Value);
            }
            if (isPromotion.HasValue && isPromotion.Value == true)
            {
                query = query.Where(b => b.PromotionEndDate < DateTime.Now);
            }
            if (languageId.HasValue) { query = query.Where(b => b.LanguageId.HasValue && b.LanguageId.Value == languageId.Value); }
            return await query.CountAsync();
        }
        public async Task<IEnumerable<Book>> GetByFilterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, int? languageId = null, int? bookCoverTypeId = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Book> query = _books;

            if (bookName != null)
            {
                query = query.Where(b => b.Title.Contains(bookName));
            }
            var y = query.ToList();
            if (categoryid.HasValue)
            {
                List<int> listcategoryId = new List<int>();
                listcategoryId.Add(categoryid.Value);
                var subCategory = _categories.Where(b => b.ParentCategoryID.HasValue && b.ParentCategoryID.Value == categoryid.Value);

                if (subCategory != null)
                {
                    foreach (var category in subCategory)
                    {
                        listcategoryId.Add(category.CategoryId);
                    }
                }
                query = query.Where(b => listcategoryId.Contains(b.CategoryId.Value));
            }
            else if (!string.IsNullOrEmpty(categoryName))
            {
                List<int> listcategoryId = new List<int>();
                var parentCategory = _categories.Where(b => b.Name.Contains(categoryName));
                //listcategoryId.Add(categoryid.Value);
                //var subCategory = _categories.Where(b => b.ParentCategoryID == categoryid);

                if (parentCategory != null)
                {
                    foreach (var category in parentCategory)
                    {
                        listcategoryId.Add(category.CategoryId);
                        var subcategories = category.SubCategories;
                        if (subcategories != null && subcategories.Count() > 0)
                        {
                            foreach (var subcategory in subcategories)
                            {
                                listcategoryId.Add(subcategory.CategoryId);

                            }
                        }
                    }
                }
                query = query.Where(b => listcategoryId.Contains(b.CategoryId.Value));
            }
            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }
            if (minQuality.HasValue)
            {
                query = query.Where(b => b.Quantity >= minQuality.Value);
            }
            if (maxQuanlity.HasValue)
            {
                query = query.Where(b => b.Quantity <= maxQuanlity.Value);
            }
            if (isPromotion.HasValue && isPromotion.Value == true)
            {
                query = query.Where(b => b.PromotionEndDate < DateTime.Now);
            }
            if (languageId.HasValue) { query = query.Where(b => b.LanguageId.HasValue && b.LanguageId.Value == languageId.Value); }
            if (bookCoverTypeId.HasValue) { query = query.Where(b => b.BookCoverTypeId.HasValue && b.BookCoverTypeId.Value == bookCoverTypeId.Value); }
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }
        public async Task<IEnumerable<Book>> GetActiveByFilterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, int? languageId = null, int? bookCoverTypeId = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Book> query = _books.Where(b => b.IsDel == false);
            if (bookName != null)
            {
                query = query.Where(b => b.Title.Contains(bookName));
            }
            if (categoryid.HasValue)
            {
                List<int> listcategoryId = new List<int>();
                listcategoryId.Add(categoryid.Value);
                var subCategory = _categories.Where(b => b.ParentCategoryID == categoryid);

                if (subCategory != null)
                {
                    foreach (var category in subCategory)
                    {
                        listcategoryId.Add(category.CategoryId);
                    }
                }
                query = query.Where(b => listcategoryId.Contains(b.CategoryId.Value));
            }
            else if (string.IsNullOrEmpty(categoryName))
            {
                List<int> listcategoryId = new List<int>();
                var parentCategory = _categories.Where(b => b.Name.Contains(categoryName));
                //listcategoryId.Add(categoryid.Value);
                //var subCategory = _categories.Where(b => b.ParentCategoryID == categoryid);

                if (parentCategory != null)
                {
                    foreach (var category in parentCategory)
                    {
                        listcategoryId.Add(category.CategoryId);
                        var subcategories = category.SubCategories;
                        if (subcategories != null && subcategories.Count() > 0)
                        {
                            foreach (var subcategory in subcategories)
                            {
                                listcategoryId.Add(subcategory.CategoryId);

                            }
                        }
                    }
                }
                query = query.Where(b => listcategoryId.Contains(b.CategoryId.Value));
            }
            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }
            if (minQuality.HasValue)
            {
                query = query.Where(b => b.Quantity >= minQuality.Value);
            }
            if (maxQuanlity.HasValue)
            {
                query = query.Where(b => b.Quantity <= maxQuanlity.Value);
            }
            if (isPromotion.HasValue && isPromotion.Value == true)
            {
                query = query.Where(b => b.PromotionEndDate < DateTime.Now);
            }
            if (languageId.HasValue) { query = query.Where(b => b.LanguageId <= languageId.Value); }
            if (bookCoverTypeId.HasValue) { query = query.Where(b => b.BookCoverTypeId <= bookCoverTypeId.Value); }
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public Task<int> CountActive()
        {
            return _books.CountAsync(x => x.IsDel == false);
        }

        public Task<int> CountDeactive()
        {
            return _books.CountAsync(x => x.IsDel == true);
        }


    }
}
