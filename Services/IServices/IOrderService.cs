using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IOrderService
    {
        Task<ServiceResult> AddAsync(OrderDto model);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> GetFilteredAsync(DateTime? minorderDate = null, DateTime? maxorderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? userId = null, string? phoneNumber = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByIdAsync(int id);
        Task<ServiceResult> UpdateAsync(OrderDto model);
        Task<ServiceResult> UpdateStauaAsync(int? userId,int id, string status);
    }
}
