using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IOtpService
    {
        Task<ServiceResult> SendPasswordResetOtpAsync(string email);
        Task<ServiceResult> SendRegistrationOtpAsync(string email);
        Task<ServiceResult> VerifyPasswordResetOtpAsync(string email, string otpCode, string newPass);
        Task<ServiceResult> VerifySendRegistrationOtpAsycn(RegisterModel model);
    }
}
