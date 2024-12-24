using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;


namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _accountService;
        private readonly IUserService _userService;
        public AuthController(IAuthService accountService, IUserService a)
        {
            _accountService = accountService;
            _userService = a;
        }
        [SwaggerOperation(Summary = "Đăng ký")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _accountService.Register(model);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Gửi OTP đăng ký")]
        [HttpPost("otp")]
        public async Task<IActionResult> Otp([FromBody] string email)
        {
            var result = await _accountService.SendOtp(email);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Đăng nhập")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _accountService.Login(model);

            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Đăng nhập")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody]string refreshToken)
        {
            var result = await _accountService.Logout(refreshToken);

            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Đăng nhập admin")]
        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModel model)
        {
            var result = await _accountService.AdminLogin(model);

            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Lấy accesstoken từ refresh token")]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await _accountService.GetTokenByRefreshToken(refreshToken);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Lấy thông tin")]
        [Authorize]
        [HttpGet("infor")]
        public async Task<IActionResult> GetInfor()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var result = await _userService.GetById(userId);

            return StatusCode(result.StatusCode, result.ApiResult);
        }

    }

}
