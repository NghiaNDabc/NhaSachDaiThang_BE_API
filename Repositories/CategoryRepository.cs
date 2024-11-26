using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<Category> _categories;
        private readonly IMapper _mapper;
        public CategoryRepository(BookStoreContext bookStoreContext, IMapper mapper)
        {
            _bookStoreContext = bookStoreContext;
            _categories = _bookStoreContext.Category;
            _mapper = mapper;
        }

        public async Task AddAsync(Category entity)
        {
            await _categories.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categories.FirstOrDefaultAsync(e => e.CategoryId == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
        public async Task SoftDelete(int id)
        {
            var category = await _categories.FirstOrDefaultAsync(e => e.CategoryId == id);
            if (category != null)
            {
                category.IsDel = !category.IsDel;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query = _categories;
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }
        public async Task<IEnumerable<Category>> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query =  _categories.Where(e => e.IsDel == false);
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categories.FirstOrDefaultAsync(e => e.CategoryId == id);
        }

        public async Task UpdateAsync(Category entity)
        {
            if (await _categories.FirstOrDefaultAsync(e => e.CategoryId == entity.CategoryId) != null)
            {
                _categories.Update(entity);
            }
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesByLevel(int? parentId = null)
        {

            var categories = await _categories
                .Where(c => c.ParentCategoryID == parentId)
                .ToListAsync();


            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);
                // Gọi đệ quy để lấy danh mục con
                categoryDto.SubCategories = (await GetCategoriesByLevel(category.CategoryId)).ToList();
                categoryDtos.Add(categoryDto);
            }

            return categoryDtos;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByLevelActive(int? parentId = null)
        {

            var categories = await _categories
                .Where(c => c.ParentCategoryID == parentId && c.IsDel == false)
                .ToListAsync();


            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);
                // Gọi đệ quy để lấy danh mục con
                categoryDto.SubCategories = (await GetCategoriesByLevelActive(category.CategoryId)).ToList();
                categoryDtos.Add(categoryDto);
            }

            return categoryDtos;
        }




        public async Task<IEnumerable<Category>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var query = _categories.Where(e => e.Name == name);
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);

        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _categories.AnyAsync(predicate);
        }

        public async Task<Category> GetActiveByIdAsync(int id)
        {
            return await _categories.Where(e => e.CategoryId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetActiveByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var query =  _categories.Where(b => b.Name.Contains(name) && b.IsDel == false);
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }
        public Task<int> CountActive()
        {
            return _categories.CountAsync(x => x.IsDel == false);
        }

        public Task<int> CountDeactive()
        {
            return _categories.CountAsync(x => x.IsDel == true);
        }
    }
}
