
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPassword : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly BookStoreContext _bookStoreContext;
        private readonly ICrudRepository<User> _userCrud;
        public ForgotPassword(EmailService emailService, BookStoreContext bookStoreContext, ICrudRepository<User> userCrud)
        {
            _emailService = emailService;
            _bookStoreContext = bookStoreContext;
            _userCrud = userCrud;
        }

        [HttpPost("otp")]
        public async Task<IActionResult> SendOtp([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { message = "Email không được để trống!" });
            }
            var user = _bookStoreContext.Users.Where(u => u.Email == email).FirstOrDefault();

            if (user == null)
            {
                return BadRequest(new { message = "Email không thuộc người dùng nào!" });
            }

            var otpCode = new Random().Next(100000, 999999).ToString();

            user.OtpCode = otpCode;
            user.OtpExpiryTime = DateTime.UtcNow.AddMinutes(5);
            _userCrud.Update(user);
            // Cấu hình nội dung email
            var subject = "Đổi mật khẩu";
            var message = $"Mã OTP của bạn là: {otpCode}";

            await _emailService.SendEmailAsync(email, subject, message);

            return Ok(new { message = "OTP được gửi thành công" });
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOTP([FromBody] string email, string otpCode, string newPass)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(
                    new OperationResult
                    {
                        Success = false,
                        Data = new
                        {
                            Messaga = "Email không được để trống"
                        }
                    }
                );
            }
            var user = _bookStoreContext.Users.Where(u => u.Email == email).FirstOrDefault();

            if (user == null)
            {
                return BadRequest(
                    new OperationResult
                    {
                        Success = false,
                        Data = new
                        {
                            Messaga = "Email không thuộc người dùng nào!"
                        }
                    }
                );
            }
            if (user.OtpCode != otpCode && user.OtpExpiryTime <= DateTime.Now)
            {
                return BadRequest(
                     new OperationResult
                     {
                         Success = false,
                         Data = new
                         {
                             Messaga = "Email không được để trống"
                         }
                     }
                 );
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _userCrud.Update(user);

            return BadRequest(
              new OperationResult
              {
                  Success = true,
                  Data = new
                  {
                      Messaga = "Email không được để trống"
                  }
              }
            );
        }
    }
}
