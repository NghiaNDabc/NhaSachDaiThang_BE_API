using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        Task<object> AddAsync(Order entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Order>> GetFilteredAsync(DateTime? orderDate=null, DateTime? deliverdDate=null,string? customerName=null, string?status = null, int? userId = null, string? phoneNumber = null,int ? pageNumber = null, int? pageSize = null);
        Task<Order> GetByIdAsync(int id);
        void Update(Order entity);
    }
}
