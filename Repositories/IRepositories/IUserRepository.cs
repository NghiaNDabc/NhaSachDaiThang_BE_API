﻿using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IUserRepository :ICrudRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
