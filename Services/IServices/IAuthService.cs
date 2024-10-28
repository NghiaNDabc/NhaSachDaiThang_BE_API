using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IAuthService
    {
        OperationResult Register(RegisterModel model);
        OperationResult Login(LoginModel model);
        OperationResult AdminLogin(LoginModel model);
        Task<OperationResult> SendOtp(string email);
        Task<User> GetCustomerByLoginModel(LoginModel model);
    }
}
