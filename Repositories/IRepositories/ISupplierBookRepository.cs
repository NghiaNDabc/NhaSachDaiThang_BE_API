using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ISupplierBookRepository 
    {
        Task AddAsync(SupplierBook entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<SupplierBook>> GetByIdAsync(int? supplierid = null, int? bookid = null);
        Task<IEnumerable<SupplierBook>> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        Task<IEnumerable<SupplierBook>> GetByNameAsync(string bookName = null, string supplierName = null, int? pageNumber = null, int? pageSize = null);
        Task UpdateAsync(SupplierBook entity);


    }
}
