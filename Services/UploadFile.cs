using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Services
{
    public class UploadFile : IUploadFile
    {
        public async Task<ServiceResult> UploadImageAsync(IFormFile file, string path)
        {
            if (file == null || file.Length == 0)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult { Success = false, Message = "File không hợp lệ." }
                };
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), path);

            // Tạo thư mục nếu không tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "File đã được tải lên thành công.",
                    Data = fileName
                }
            };
        }

        public ServiceResult DeleteFile(string fileName, string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            if (!File.Exists(filePath))
            {
                return ServiceResultFactory.NotFound(("File không tồn tại."));
            }

            File.Delete(filePath);

            return ServiceResultFactory.Ok("File đã được xóa thành công.");


        }

    }
}
