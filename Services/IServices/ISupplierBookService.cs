using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ISupplierBookService
    {
        Task<ServiceResult> AddRangeAsync(IEnumerable<SupplierBookDto> models);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetBySuppierIdAsync(int supplierBookId, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByFilterAsync(int? supplierId = null, int? bookId = null, string? bookName = null, string? supplierName = null, DateTime? minDate = null, DateTime? maxDate = null, int? pageNumber = null, int? pageSize = null);
        //Task<ServiceResult> Update(SupplierBookDto model);
    }

}
