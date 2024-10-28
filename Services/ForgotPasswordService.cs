using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;
namespace NhaSachDaiThang_BE_API.Services
{
    public class ForgotPasswordService :IOtpService
    {
        private readonly EmailService _emailService;
        private  IMemoryCache _cache;
        private readonly IUserRepository _userRepository;
        public ForgotPasswordService(EmailService emailService,  IMemoryCache memoryCache, IUserRepository userRepository)
        {
            _emailService = emailService;
            _cache = memoryCache;
            _userRepository = userRepository;

        }

        public async Task<OperationResult> SendOtp( string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new OperationResult
                {
                    Success = false,
                    Data = new
                    {
                        message = "Email không được để trống!"
                    }
                };
            }

            var user = _userRepository.GetByEmail(email); 
            if (user == null)
            {
                return new OperationResult
                {
                    Success = false,
                    Data = new
                    {
                        message = "Email không thuộc người dùng nào!!"
                    }
                };
            }

            var otpCode = new Random().Next(100000, 999999).ToString();

            var cacheKey = $"OTP_{email}";

            _cache.Set(cacheKey, otpCode, TimeSpan.FromMinutes(5));

            //  nội dung email
            var subject = "Đổi mật khẩu";
            var message = $"Mã OTP của bạn là: {otpCode}";

            await _emailService.SendEmailAsync(email, subject, message);

            return new OperationResult
            {
                Success = true,
                Data = new
                {
                    message = "OTP được gửi thành công"
                }
            };
        }

        public OperationResult VerifyOtp( string email, string otpCode, string newPass)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return
                    new OperationResult
                    {
                        Success = false,
                        Data = new
                        {
                            Messaga = "Email không được để trống"
                        }
                    }
                ;
            }

            var user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                return
                    new OperationResult
                    {
                        Success = false,
                        Data = new
                        {
                            Messaga = "Email không thuộc người dùng nào!"
                        }
                    };
                
            }

            string catchKey = $"OTP_{email}";
            if (!_cache.TryGetValue(catchKey, out string? catchOTP) || catchOTP!= otpCode)
            {
                return (
                     new OperationResult
                     {
                         Success = false,
                         Data = new
                         {
                             Messaga = "OTP không hợp lệ hoặc đã hết hạn"
                         }
                     }
                 );
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPass);
            _userRepository.Update(user);

            return
              new OperationResult
              {
                  Success = true,
                  Data = new
                  {
                      Messaga = "Đổi mật khẩu thành công"
                  }
              };
           
        }
    }
}
