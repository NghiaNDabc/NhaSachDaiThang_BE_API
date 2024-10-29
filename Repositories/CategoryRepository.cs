using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using System.Linq.Expressions;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<Category> _categories;

        public  CategoryRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _categories = _bookStoreContext.Categories;
        }

        public async Task AddAsync(Category entity)
        {
            await _categories.AddAsync(entity);       }

        public async Task DeleteAsync(int id)
        {
            var category = await _categories.FirstOrDefaultAsync(e=>e.CategoryId == id);
            if(category!=null)
            {
                _categories.Remove(category);
            }
        }
        public async Task SoftDelete(int id)
        {
            var category = await _categories.FirstOrDefaultAsync(e => e.CategoryId == id);
            if (category != null)
            {
                category.IsDel = true;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetActiveAsync()
        {
            return  await _categories.Where(e=>e.IsDel==false).ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categories.FirstOrDefaultAsync(e => e.CategoryId == id);
        }


        public async Task UpdateAsync(Category entity)
        {
            if(await _categories.FirstOrDefaultAsync(e => e.CategoryId == entity.CategoryId) != null)
            {
                _categories.Update(entity);
            }
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _categories.FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _categories.AnyAsync(predicate);
        }
    }
}
