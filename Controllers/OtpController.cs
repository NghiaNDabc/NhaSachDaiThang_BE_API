
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
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _forgotPasswordService;
        public OtpController(IOtpService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost("otp")]
        public async Task<IActionResult> SendOtp([FromBody] string email)
        {
            var result = await _forgotPasswordService.SendOtp(email);
            if (result.Success ==false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP([FromBody] ForgotPassDTO forgotPassDTO)
        {
            var result = _forgotPasswordService.VerifyOtp(forgotPassDTO.email, forgotPassDTO.otpCode, forgotPassDTO.newPass);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
