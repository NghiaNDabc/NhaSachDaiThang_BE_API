using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IUserRepository :ICrudRepository<User>, IGetByName<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllEmployeesAsync();
        Task<IEnumerable<User>> GetAllAdminAsync();
        Task<User> GetByEmail(string email);
        Task<User> GetByRefreshToken(string refreshToken);
    }
}
