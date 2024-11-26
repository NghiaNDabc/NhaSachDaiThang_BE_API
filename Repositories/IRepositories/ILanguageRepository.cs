using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ILanguageRepository
    {
        Task<object> AddAsync(Language entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Language>> GetByNameAsync(string name);
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language> GetByIdAsync(int id);
        void Update(Language entity);
    }
}
