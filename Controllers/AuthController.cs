using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;


namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _accountService;

        public AuthController(IAuthService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _accountService.Register(model);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpPost("otp")]
        public async Task<IActionResult> Otp(string email)
        {
            var result = await _accountService.SendOtp(email);
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _accountService.Login(model);

            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModel model)
        {
            var result = await _accountService.Login(model);

            return StatusCode(result.StatusCode, result.ApiResult);
        }

    }

}
