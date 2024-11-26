using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IUploadFile
    {
        Task<ServiceResult> UploadImageAsync(IFormFile file, string path);
        ServiceResult DeleteFile(string fileName, string path);
    }
}
