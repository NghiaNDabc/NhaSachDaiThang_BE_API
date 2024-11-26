using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ISupplierBookRepository 
    {
        Task AddAsync(SupplierBook entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<SupplierBook>> GetByIdAsync(int supplierbookid);
        Task<IEnumerable<SupplierBook>> GetAllAsync();
        Task<IEnumerable<SupplierBook>> GetByFilterAsync(int?bookId=null, int?supplierId=null,string? bookName = null, string? supplierName = null, DateTime? minDate = null, DateTime? maxDate = null);
        Task UpdateAsync(SupplierBook entity);
        Task<int> GetNextSupplierBookIdAsync();


    }
}
