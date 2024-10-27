using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;
        public AccountService( IUserRepository userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<OperationResult> Register(RegisterModel model)
        {
            if (_userRepository.GetByEmail( model.Email) !=null)
            {
                return new OperationResult
                {
                    Success = false,
                    Data = new { Message = "Email đã tồn tại" }
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var customer = new User
            {
                FirstName = model.FullName,
                Email = model.Email,
                PasswordHash = passwordHash,
                Phone = model.Phone,
                Address = model.Address,
                RoleId = model.RoleID
            };

             _userRepository.Add(customer);

            return new OperationResult
            {
                Success = true,
                Data = new { Message = "Đăng ký thành công." }
            }; 
        }

        public async Task<OperationResult> Login(LoginModel model)
        {
            var user = _userRepository.GetByEmail( model.Email);

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

        public async Task<OperationResult> AdminLogin(LoginModel model)
        {
            var user = _userRepository.GetByEmail(model.Email);

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
