using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IAccountService
    {
        Task<OperationResult> Register(RegisterModel model);
        Task<OperationResult> Login(LoginModel model);
        Task<OperationResult> AdminLogin(LoginModel model);
        Task<User> GetCustomerByLoginModel(LoginModel model);
    }
}
