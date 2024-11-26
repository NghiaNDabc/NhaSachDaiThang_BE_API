using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IBookCoverTypeService
    {
        Task<ServiceResult> AddAsync(BookCoverTypeDto model);

        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> GetAllAsync();
        Task<ServiceResult> GetByIdAsync(int id);
        Task<ServiceResult> UpdateAsync(BookCoverTypeDto model);
    }
}
