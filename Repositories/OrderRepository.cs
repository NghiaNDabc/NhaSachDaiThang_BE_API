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
            return await _orders.Where(x=>x.OrderId==id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetFilteredAsync(DateTime? minorderDate = null, DateTime? maxorderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null,int? userId=null, string? phoneNumber=null)
        {
            IQueryable<Order> query = _orders.OrderByDescending(x=>x.CreatedDate);
            if(minorderDate != null)
            {
                ////minorderDate = minorderDate.Value.Date.Add(minorderDate.Value.TimeOfDay);
                query = query.Where(o=> o.CreatedDate.Value.Date >= minorderDate.Value.Date); 
            }
            if (maxorderDate != null)
            {
               // maxorderDate = maxorderDate.Value.Date.Add(maxorderDate.Value.TimeOfDay);
                query = query.Where(o => o.CreatedDate.Value.Date <= maxorderDate.Value.Date);
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
            if (userId != null)
            {
                query = query.Where(o => o.UserId == userId);
            }
            if (phoneNumber != null)
            {
                query = query.Where(o => o.Phone == phoneNumber);
            }
            return await query.ToListAsync();
        }

        public void Update(Order entity)
        {
           _orders.Update(entity);
        }
    }
}
