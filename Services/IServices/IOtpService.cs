using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IOtpService
    {
        Task<ServiceResult> SendPasswordResetOtp(string email);
        Task<ServiceResult> SendRegistrationOtp(string email);
        Task<ServiceResult> VerifyPasswordResetOtp(string email, string otpCode, string newPass);
        Task<ServiceResult> VerifySendRegistrationOtp(RegisterModel model);
    }
}
