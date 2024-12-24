
using Microsoft.Extensions.Caching.Memory;

using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;
using System.Text.RegularExpressions;
namespace NhaSachDaiThang_BE_API.Services
{
    public class OtpService :IOtpService
    {
        private readonly EmailHelper _emailService;
        private  IMemoryCache _cache;
        private readonly IUnitOfWork _unitOfWork;
        public OtpService(EmailHelper emailService,  IMemoryCache memoryCache, IUnitOfWork userRepository)
        {
            _emailService = emailService;
            _cache = memoryCache;
            _unitOfWork = userRepository;

        }
       public static ServiceResult ValidEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email không được để trống!"
                    }
                };
            }
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, emailPattern)) {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Địa chỉ email không hợp lệ"
                    }
                };
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email không được để trống!"
                    }
                };
            }
            return new ServiceResult
            {
                StatusCode = 200
            };

        }
        public async Task<ServiceResult> SendPasswordResetOtpAsync( string email)
        {
            
            var check = ValidEmail(email);
            if (check.StatusCode == 400) return check;
            var user = _unitOfWork.UserRepository.GetByEmail(email); 
            if (user == null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email không thuộc người dùng nào!!"
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

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "OTP được gửi thành công"
                }
            };
        }

        public async Task<ServiceResult> SendRegistrationOtpAsync(string email)
        {
            var check = ValidEmail(email);
            if (check.StatusCode == 400) return check;

            var user = _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email đã tồn tại!"
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

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "OTP được gửi thành công"
                }
            };
        }

        public async Task<ServiceResult> VerifyPasswordResetOtpAsync( string email, string otpCode, string newPass)
        {
            var check = ValidEmail(email);
            if (check.StatusCode == 400) return check;

            var  user = await _unitOfWork.UserRepository.GetByEmail(email);
            if (user == null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email không thuộc người dùng nào"
                    }
                };

            }

            string catchKey = $"OTP_{email}";
            if (!_cache.TryGetValue(catchKey, out string? catchOTP) || catchOTP!= otpCode)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Mã OTP sai hoặc hết hạn"
                    }
                };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPass);
            user.ModifyDate = DateTime.Now;
            user.ModifyBy = "User";
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Đổi mật khẩu thành công"
                }
            };

        }
        public async Task<ServiceResult> VerifySendRegistrationOtpAsycn(RegisterDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email không được để trống"
                    }
                };
            }

            var user = await _unitOfWork.UserRepository.GetByEmail(model.Email);
            if (user != null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Email đã được sử dụng"
                    }
                };

            }

            string catchKey = $"OTP_{model.Email}";
            if (!_cache.TryGetValue(catchKey, out string? catchOTP) || catchOTP != model.Otp)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "OTP không hợp lệ hoặc đã hết hạn"
                    }
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var customer = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = passwordHash,
                Phone = model.Phone,
                Address = model.Address,
                RoleId = 3
            };
            await _unitOfWork.UserRepository.AddAsync(customer);
            await _unitOfWork.SaveChangeAsync();

              return new ServiceResult
              {
                  StatusCode = 200,
                  ApiResult = new ApiResult
                  {
                      Success = true,
                      Message = "Đăng ký thành công"
                  }
              };

        }

  
    }
}
