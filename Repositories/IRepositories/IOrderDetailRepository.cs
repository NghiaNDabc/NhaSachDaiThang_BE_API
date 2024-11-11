using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IOrderDetailRepository
    {
        Task AddAsync(OrderDetail entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int orderId);
        void RemoveRange(IEnumerable<OrderDetail> entity);
        Task UpdateAsync(OrderDetail entity);
    }
}
