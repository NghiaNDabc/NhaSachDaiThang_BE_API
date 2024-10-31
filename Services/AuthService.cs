using AutoMapper;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;
using NuGet.Common;

namespace NhaSachDaiThang_BE_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHelper _jwtHelper;
        private readonly IOtpService _otpService;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork, JwtHelper jwtHelper, IOtpService otpService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
            _otpService = otpService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> SendOtp(string email)
        {
            return await _otpService.SendRegistrationOtp(email);
        }

        public async  Task<ServiceResult> Register(RegisterModel model)
        {
            return await _otpService.VerifySendRegistrationOtp(model); 
        }

        public async Task<ServiceResult> Login(LoginModel model)
        {
            User user = await _unitOfWork.UserRepository.GetByEmail(model.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Tên đăng nhập hoặc mật khẩu không đúng"
                    }
                };
            }
            var token = _jwtHelper.GenerateJwtToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = new
                    {
                        User = _mapper.Map<UserDTO>(user),
                        Token = token,
                        RefreshToken = refreshToken
                    }
                }
            };
             
        }

        public async Task<ServiceResult> AdminLogin(LoginModel model)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(model.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Tên đăng nhập hoặc mật khẩu không đúng"
                    }
                };
            }

            var token = _jwtHelper.GenerateJwtToken(user);
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = new
                    {
                        User = _mapper.Map<UserDTO>(user),
                        Token = token
                    }
                }
            };
        }

        public Task<User> GetCustomerByLoginModel(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetTokenByRefreshToken(string refreshToken)
        {
            User user = await _unitOfWork.UserRepository.GetByRefreshToken(refreshToken);

            if (user == null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Refresh token không hợp lệ hoặc đã hết hạn"
                    }
                };
            }

            var newToken = _jwtHelper.GenerateJwtToken(user);
            //var newRefreshToken = _jwtHelper.GenerateRefreshToken();

            //user.RefreshToken = newRefreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); // Hoặc bất kỳ thời gian hết hạn nào bạn muốn
            //await _unitOfWork.UserRepository.UpdateAsync(user);
            //await _unitOfWork.SaveChangeAsync();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = new
                    {
                        Token = newToken
                    }
                }
            };
        }

    }
}
