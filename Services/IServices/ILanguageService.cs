using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ILanguageService
    {
        Task<ServiceResult> AddAsync(LanguageDto model);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> GetAllAsync();
        Task<ServiceResult> GetByIdAsync(int id);
        Task<ServiceResult> UpdateAsync(LanguageDto model);
    }
}
