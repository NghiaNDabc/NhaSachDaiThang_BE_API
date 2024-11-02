using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.CommonInterface
{
    public interface ICrudService<T> where T : class
    {
        Task<ServiceResult> Add(T model);
        Task<ServiceResult> Update(T model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        
    }
}
