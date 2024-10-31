using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IAuthService
    {
        Task<ServiceResult> Register(RegisterModel model);
        Task<ServiceResult> Login(LoginModel model);
        Task<ServiceResult> AdminLogin(LoginModel model);
        Task<ServiceResult> SendOtp(string email);
        Task<ServiceResult> GetTokenByRefreshToken(string refreshToken);
        Task<User> GetCustomerByLoginModel(LoginModel model);
    }
}
