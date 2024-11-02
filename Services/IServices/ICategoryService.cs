using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ICategoryService : IActiveService
    {
        Task<ServiceResult> Add(CategoryDto model);
        Task<ServiceResult> Update(CategoryDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
    }
}
