using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ISupplierBookService
    {
        Task<ServiceResult> Add(SupplierBookDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByIdAsync(int? bookId = null, int? supplierId = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByNameAsync(string bookName = null, string supplierName = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> Update(SupplierBookDto model);
    }

}
