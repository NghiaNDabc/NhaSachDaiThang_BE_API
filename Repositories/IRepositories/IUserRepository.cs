using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IUserRepository :ICrudRepository<User>, IGetByName<User>  
    {
        Task<IEnumerable<User>> GetAllAsync(AccountType accountType, int? pageNumber = null, int? pageSize = null);
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetByName( string name, AccountType? accountType, int? pageNumber = null, int? pageSize = null);
        Task<User> GetByRefreshToken(string refreshToken);
    }
}
