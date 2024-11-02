using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.CommonInterface
{
    public interface IActiveService
    {
        Task<ServiceResult> GetAllActive();
        Task<ServiceResult> GetActiveById(int id);
        Task<ServiceResult> SoftDelete(int id);
        Task<ServiceResult> GetActiveByName(string name);
    }
}
