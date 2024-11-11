using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IOrderService
    {
        Task<ServiceResult> AddAsync(OrderDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetFilteredAsync(DateTime? orderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByIdAsync(int id);
        Task<ServiceResult> Update(OrderDto model);
    }
}
