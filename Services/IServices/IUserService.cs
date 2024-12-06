using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IUserService :ICrudService<UserDTO>, IGetByNameService, IActiveService
    {
        Task<ServiceResult> AddAsync(UserDTO model, IFormFile formFile);
        Task<ServiceResult> UpdateAsync(UserDTO model, IFormFile imageFiles);
        Task<ServiceResult> ClientUpdateAsync(UserDTO model);
        Task<ServiceResult> ClientChangePassAsync(ChangePassDto model);
        Task<ServiceResult> GetAllAsync(AccountType accountType, int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetByNameAsync(string name, AccountType accountType, int? pageNumber = null, int? pageSize = null);
    }
}
