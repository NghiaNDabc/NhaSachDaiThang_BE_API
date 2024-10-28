using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface IOtpService
    {
        Task<OperationResult> SendOtp(string email);
        OperationResult VerifyOtp(string email, string otpCode, string newPass);
    }
}
