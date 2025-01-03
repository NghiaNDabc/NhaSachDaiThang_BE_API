﻿using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IUserRepository 
    {
        Task AddAsync(User entity);
        Task DeleteAsync(int id);
        Task ChangStautsAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllAsync(AccountType accountType, int? pageNumber = null, int? pageSize = null);
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User entity);
        Task<User> GetByEmail(string email);
        Task<User> GetByRefreshToken(string refreshToken);
        Task<IEnumerable<User>> GetByNameAsync(string name);
        Task<IEnumerable<User>> GetByNameAsync(string name, AccountType? accountType = null);
    }
}
