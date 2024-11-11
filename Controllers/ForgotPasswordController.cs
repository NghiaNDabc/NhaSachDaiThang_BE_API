
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IOtpService _forgotPasswordService;
        public ForgotPasswordController(IOtpService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }
        [SwaggerOperation(Summary = "Gửi OTP quên mật khẩu")]
        [HttpPost("otp")]
        public async Task<IActionResult> SendOtp([FromBody] string email)
        {
            var result = await _forgotPasswordService.SendPasswordResetOtpAsync(email);
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOTP([FromBody] ForgotPassDTO forgotPassDTO)
        {
            var result =await _forgotPasswordService.VerifyPasswordResetOtpAsync(forgotPassDTO.email, forgotPassDTO.otpCode, forgotPassDTO.newPass);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
    }
}
