using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.CommonInterface
{
    public interface IGetByNameService
    {
        Task<ServiceResult> GetByNameAsync(string name);
    }
}
