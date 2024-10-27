using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;


namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _accountService.Register(model);
            if (result.Success)
            {
                return Ok(result);
            } 
             return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _accountService.Login(model);

            if (result.Success == false)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModel model)
        {
            var result = await _accountService.Login(model);

            if (result.Success == false)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

    }

}
