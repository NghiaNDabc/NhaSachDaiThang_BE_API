using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ICategoryService
    {
        Task<ServiceResult> Add(CategoryDto model);
        Task<ServiceResult> Update(CategoryDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> SoftDelete(int id);
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetAllActive();
    }
}
