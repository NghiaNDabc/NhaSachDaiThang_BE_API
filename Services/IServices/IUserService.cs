using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IUserService :ICrudService<UserDTO>, IGetByNameService, IActiveService
    {
        Task<ServiceResult> Add(UserDTO model, IFormFile formFile);
        Task<ServiceResult> Update(UserDTO model, IFormFile imageFiles);
        Task<ServiceResult> GetAll(AccountType accountType);
    }
}
