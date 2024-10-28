using Microsoft.AspNetCore.Identity;
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
    public class OtpService :IOtpService
    {
        private readonly EmailHelper _emailService;
        private  IMemoryCache _cache;
        private readonly IUserRepository _userRepository;
        public OtpService(EmailHelper emailService,  IMemoryCache memoryCache, IUserRepository userRepository)
        {
            _emailService = emailService;
            _cache = memoryCache;
            _userRepository = userRepository;

        }

        public async Task<OperationResult> SendPasswordResetOtp( string email)
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

        public async Task<OperationResult> SendRegistrationOtp(string email)
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
            if (user != null)
            {
                return new OperationResult
                {
                    Success = false,
                    Data = new
                    {
                        message = "Email đã được sử dụng!"
                    }
                };
            }

            var otpCode = new Random().Next(100000, 999999).ToString();

            var cacheKey = $"OTP_{email}";

            _cache.Set(cacheKey, otpCode, TimeSpan.FromMinutes(5));

            //  nội dung email
            var subject = "Xác nhận đăng ký";
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

        public OperationResult VerifyPasswordResetOtp( string email, string otpCode, string newPass)
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
        public OperationResult VerifySendRegistrationOtp(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
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

            var user = _userRepository.GetByEmail(model.Email);
            if (user != null)
            {
                return
                    new OperationResult
                    {
                        Success = false,
                        Data = new
                        {
                            Messaga = "Email đã được sử dụng!"
                        }
                    };

            }

            string catchKey = $"OTP_{model.Email}";
            if (!_cache.TryGetValue(catchKey, out string? catchOTP) || catchOTP != model.Otp)
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

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var customer = new User
            {
                FirstName = model.FullName,
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = passwordHash,
                Phone = model.Phone,
                Address = model.Address,
                RoleId = model.RoleID
            };
            _userRepository.Add(customer);

            return
              new OperationResult
              {
                  Success = true,
                  Data = new
                  {
                      Messaga = "Đăng ký thành công."
                  }
              };

        }
    }
}
