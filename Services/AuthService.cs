using AutoMapper;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IUnitOfWork unitOfWork, JwtHelper jwtHelper, IOtpService otpService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
            _otpService = otpService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResult> SendOtp(string email)
        {
            return await _otpService.SendRegistrationOtpAsync(email);
        }

        public async  Task<ServiceResult> Register(RegisterModel model)
        {
            return await _otpService.VerifySendRegistrationOtpAsycn(model); 
        }

        public async Task<ServiceResult> Login(LoginModel model)
        {
            User user = await _unitOfWork.UserRepository.GetByEmail(model.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash) || user.RoleId != 3)
            {
                return ServiceResultFactory.BadRequest("Tên đăng nhập hoặc mật khẩu không đúng");
            }
            var token = _jwtHelper.GenerateJwtToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
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
                return ServiceResultFactory.BadRequest("Tên đăng nhập hoặc mật khẩu không đúng");
            }
            var token = _jwtHelper.GenerateJwtToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            var userDto = _mapper.Map<UserDTO>(user);
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            userDto.Image = $"{baseUrl}/{GlobalConst.UserImageRelativePath}/{userDto.Image}";
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync();
            var Data = new
            {
                User = userDto,
                Token = token,
                RefreshToken = refreshToken
            };
            return ServiceResultFactory.Ok(data: Data);
        }

        public Task<User> GetCustomerByLoginModel(LoginModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> Logout(string refreshToken)
        {
            // Retrieve the user by refresh token
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

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Đăng xuất thành công"
                }
            };
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
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); 
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
                        Token = newToken,
                        User = _mapper.Map<UserDTO>(user)
                    }
                }
            };
        }

    }
}
