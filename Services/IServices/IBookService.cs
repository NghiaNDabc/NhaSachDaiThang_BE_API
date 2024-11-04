using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IBookService
    {
        Task<ServiceResult> Add(BookDto model, List<IFormFile> imageFiles);
        Task<ServiceResult> Add(BookDto model);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetAllActive(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> SoftDelete(int id);
        Task<ServiceResult> Update(BookDto model);
        Task<ServiceResult> Update(BookDto model, List<IFormFile> imageFiles);
        Task<ServiceResult> GetActiveById(int id);
        Task<ServiceResult> GetByNameAndCategoryIdAsync(int? categoryId = null, string? name = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetActiveByNameAndCategoryIdAsync(int? categoryId = null, string? name = null, int? pageNumber = null, int? pageSize = null);
    }
}
