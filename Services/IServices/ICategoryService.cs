using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ICategoryService 
    {
        Task<ServiceResult> Add(CategoryDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetActiveById(int id);
        Task<ServiceResult> GetActiveByName(string name, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetAllActive(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> SoftDelete(int id);
        Task<ServiceResult> Update(CategoryDto model);
    }
}
