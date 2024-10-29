using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHelper _jwtHelper;
        private readonly IOtpService _otpService;
        public AuthService(IUnitOfWork unitOfWork, JwtHelper jwtHelper, IOtpService otpService)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
            _otpService = otpService;
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


            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = new
                    {
                        User = new UserDTO
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            Address = user.Address,
                            RoleName = user.Role.RoleName
                        },
                        Token = token
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

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = new
                    {
                        user = new UserDTO
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            Address = user.Address,
                            RoleName = user.Role.RoleName
                        }
                    }
                }
            };
        }

        public Task<User> GetCustomerByLoginModel(LoginModel model)
        {
            throw new NotImplementedException();
        }

    }
}
