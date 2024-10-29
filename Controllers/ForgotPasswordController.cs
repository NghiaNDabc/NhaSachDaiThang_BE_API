
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IOtpService _forgotPasswordService;
        public ForgotPasswordController(IOtpService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost("otp")]
        public async Task<IActionResult> SendOtp([FromBody] string email)
        {
            var result = await _forgotPasswordService.SendPasswordResetOtp(email);
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOTP([FromBody] ForgotPassDTO forgotPassDTO)
        {
            var result =await _forgotPasswordService.VerifyPasswordResetOtp(forgotPassDTO.email, forgotPassDTO.otpCode, forgotPassDTO.newPass);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
    }
}
