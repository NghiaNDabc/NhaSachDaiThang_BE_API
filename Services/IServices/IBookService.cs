using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IBookService
    {
        Task<ServiceResult> AddAsync(BookDto model, List<IFormFile> imageFiles);
        Task<ServiceResult> AddAsync(BookDto model);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByIdAsync(int id);
        Task<ServiceResult> ChangStatusAsync(int id);
        Task<ServiceResult> UpdateAsync(BookDto model);
        Task<ServiceResult> UpdateAync(BookDto model, List<IFormFile> imageFiles);
        Task<ServiceResult> CountAsync();
        Task<ServiceResult> GetActiveByIdAsync(int id);
        Task<ServiceResult> GetNewBooks();
        Task<ServiceResult> GetByFilterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, bool? isActive = null, int? languageId = null, int? bookCoverTypeId = null, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetActiveByFilterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, int? languageId = null, int? bookCoverTypeId = null, int? pageNumber = null, int? pageSize = null);
    }
}
