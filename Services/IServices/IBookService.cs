using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IBookService :ICrudService<BookDto>, IActiveService, IGetByNameService
    {
        Task<ServiceResult> UploadImage(IFormFile file);
        Task<ServiceResult> GetAllActive();
        Task<ServiceResult> GetBooksByCategoryId(int id);
    }
}
