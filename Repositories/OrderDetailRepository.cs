using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Entities;
using System.Collections.Specialized;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class OrderDetailRepository : NhaSachDaiThang_BE_API.Repositories.IRepositories.IOrderDetailRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private DbSet<OrderDetail> _orderDetails;
        public OrderDetailRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _orderDetails = _bookStoreContext.OrderDetail;
        }

        public async Task AddAsync(OrderDetail entity)
        {
          await  _orderDetails.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _orderDetails.FindAsync(id);
            if(item !=null)
             _orderDetails.Remove(item);
        }

        public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int id)
        {
            return await _orderDetails.Where(x=> x.OrderId == id).ToListAsync();
        }

        public void RemoveRange(IEnumerable<OrderDetail> entity)
        {
            _orderDetails.RemoveRange(entity);  
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            if (await _orderDetails.FindAsync(entity.OrderDetailId) != null)
                _orderDetails.Update(entity);
        }
    }
}
