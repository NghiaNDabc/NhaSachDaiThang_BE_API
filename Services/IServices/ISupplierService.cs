

using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ISupplierService
    {
        Task<ServiceResult> Add(SupplierDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetByFilterAsync(string name, bool?isDel = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> ChangeStatus(int id);
        Task<ServiceResult> Update(SupplierDto model);
    }
}
