using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IOtpService
    {
        Task<OperationResult> SendPasswordResetOtp(string email);
        Task<OperationResult> SendRegistrationOtp(string email);
        OperationResult VerifyPasswordResetOtp(string email, string otpCode, string newPass);
        OperationResult VerifySendRegistrationOtp(RegisterModel model);
    }
}
