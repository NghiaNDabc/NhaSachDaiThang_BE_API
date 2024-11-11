using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<Order> _orders;
        public OrderRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _orders = _bookStoreContext.Order;
        }

        public async Task<object> AddAsync(Order entity)
        {
           return await _orders.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _orders.FindAsync(id);
            if (item != null) _orders.Remove(item);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetFilteredAsync(DateTime? orderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Order> query = _orders;
            if(orderDate != null)
            {
                query = query.Where(o=> o.OrderDate == orderDate); 
            }
            if (deliverdDate != null)
            {
                query = query.Where(o => o.DeliveredDate == deliverdDate);
            }
            if (customerName != null)
            {
                query = query.Where(o => o.Customer.FirstName.Contains(customerName) || o.Customer.LastName.Contains(customerName));
            }
            if (status != null)
            {
                query = query.Where(o => o.Status == status);
            }

            return await PaginationHelper.PaginateAsync(query);
        }

        public void Update(Order entity)
        {
           _orders.Update(entity);
        }
    }
}
