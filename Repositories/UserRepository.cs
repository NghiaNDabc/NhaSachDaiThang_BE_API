using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace NhaSachDaiThang_BE_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BookStoreContext _bookStoreContext;
        private DbSet<User> _users;

        public UserRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _users = bookStoreContext.User;
        }

        public async Task AddAsync(User entity)
        {
            await _users.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _users.FindAsync(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            if (pageNumber == null || pageNumber == 0)
                return await _users.ToListAsync();
            int defaultPageSize = 10;
            int pageNum = pageNumber ?? 1;
            int size = pageSize ?? defaultPageSize;

            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            int skip = (pageNum - 1) * size;
            if (size > 0)
            {
                return await _users
                    .Skip(skip)
                    .Take(size)
                    .ToListAsync();
            }
            else
            {

                return await _users.ToListAsync();
            }
        }
        public async Task<IEnumerable<User>> GetAllAsync(AccountType accountType,int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<User> userQuery;
            switch (accountType)
            {
                case AccountType.Admin:
                    userQuery = _users.Where(u => u.RoleId == 1); break;
                case AccountType.Employee:
                    userQuery = _users.Where(u => u.RoleId == 2); break;
                case AccountType.Customer:
                    userQuery = _users.Where(u => u.RoleId == 2); break;
                default: userQuery = _users;break;
            }

            return await PaginationHelper.PaginateAsync(userQuery, pageNumber, pageSize);
        }
        public Task<User> GetByIdAsync(int id)
        {
            var user = _users.Where(u => u.UserId == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _users.OrderBy(e => e.UserId).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            if (await _users.FirstOrDefaultAsync(u => u.UserId == entity.UserId) != null)
                _users.Update(entity);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByRefreshToken(string refreshToken)
        {
            return await _users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.Now);
        }

        public async Task<IEnumerable<User>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var query = _users.Where(b => (b.FirstName + " " + b.LastName).Contains(name));
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<User>> GetByName(string name, AccountType? accountType=null, int? pageNumber = null, int?pageSzie = null)
        {
            IQueryable<User> users =_users.Where(b => (b.FirstName + " " + b.LastName).Contains(name));
            switch (accountType) {
                case AccountType.Admin: users= users.Where(x => x.RoleId == 1); break;
                case AccountType.Customer: users= users.Where(x => x.RoleId == 3); break;
                case AccountType.Employee: users= users.Where(x => x.RoleId == 2); break;
            }
            return await users.ToListAsync();
        }
    }
}
