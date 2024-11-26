using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using static NuGet.Packaging.PackagingConstants;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<Language> _languages;
        public LanguageRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _languages = _bookStoreContext.Language;
        }
        public async Task<object> AddAsync(Language entity)
        {
            return await _languages.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _languages.FindAsync(id);
            if (item != null) _languages.Remove(item);
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _languages.ToListAsync();
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _languages.FindAsync(id);
        }

        public async Task<IEnumerable<Language>> GetByNameAsync(string name)
        {
           return await _languages.Where(x => x.Name == name).ToListAsync();
        }

        public void Update(Language entity)
        {
            _languages.Update(entity);
        }
    }
}
