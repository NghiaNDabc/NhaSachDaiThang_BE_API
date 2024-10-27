using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IUserRepository :ICrudRepository<User>
    {
        User GetByEmail(string email);
    }
}
