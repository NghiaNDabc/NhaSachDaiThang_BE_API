using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using System.Linq.Expressions;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<Category> _categories;
        private readonly IMapper _mapper;
        public  CategoryRepository(BookStoreContext bookStoreContext, IMapper mapper)
        {
            _bookStoreContext = bookStoreContext;
            _categories = _bookStoreContext.Category;
            _mapper = mapper;
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
        public async Task<IEnumerable<CategoryDto>> GetCategoriesByLevel(int? parentId = null)
        {
            // Tìm tất cả các Category có ParentCategoryID là parentId
            var categories = await _categories
                .Where(c => c.ParentCategoryID == parentId)
                .ToListAsync();

            // Ánh xạ từ Category sang CategoryDto, bao gồm cả các danh mục con
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
            // Tìm tất cả các Category có ParentCategoryID là parentId
            var categories = await _categories
                .Where(c => c.ParentCategoryID == parentId && c.IsDel == false)
                .ToListAsync();

            // Ánh xạ từ Category sang CategoryDto, bao gồm cả các danh mục con
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




        public async Task<IEnumerable<Category>> GetByNameAsync(string name)
        {
            return await _categories.Where(e => e.Name == name).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _categories.AnyAsync(predicate);
        }
    }
}
