using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly IOtpService _otpService;
        public AuthService( IUserRepository userRepository, JwtHelper jwtHelper, IOtpService otpService)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _otpService = otpService;
        }

        public async Task<OperationResult> SendOtp(string email)
        {
            return await _otpService.SendRegistrationOtp(email);
        }

        public  OperationResult Register(RegisterModel model)
        {
            return _otpService.VerifySendRegistrationOtp(model); 
        }

        public OperationResult Login(LoginModel model)
        {
            var user = _userRepository.GetByEmail( model.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new OperationResult
                {
                    Success = false,
                    Data = new
                    {
                        Message= "Tên đăng nhập hoặc mật khẩu không đúng"
                    }
                };
            }
            var token = _jwtHelper.GenerateJwtToken(user);
            return new OperationResult
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
            };
             
        }

        public OperationResult AdminLogin(LoginModel model)
        {
            var user = _userRepository.GetByEmail(model.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new OperationResult
                {
                    Success = false,
                    Data = new
                    {
                        Message = "Tên đăng nhập hoặc mật khẩu không đúng"
                    }
                };
            }

            return new OperationResult
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
            };
        }

        public Task<User> GetCustomerByLoginModel(LoginModel model)
        {
            throw new NotImplementedException();
        }

    }
}
