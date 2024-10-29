using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ICategoryService
    {
        Task<ServiceResult> Add(Category model);
        Task<ServiceResult> Update(Category model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> SoftDelete(int id);
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetAllActive();
    }
}
