using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ISupplierRepository 
    {
        Task AddAsync(Supplier entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        Task<Supplier> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetByFilterAsync(string? name=null,bool? isDel = null, int? pageNumber = null, int? pageSize = null);
        Task ChangeStatusAsync(int id);
        Task UpdateAsync(Supplier entity);
    }
}
