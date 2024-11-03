using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IUploadFile
    {
        Task<ServiceResult> UploadImage(IFormFile file, string path);
    }
}
