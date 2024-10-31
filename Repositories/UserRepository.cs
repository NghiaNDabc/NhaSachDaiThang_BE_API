using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BookStoreContext _bookStoreContext;
        private DbSet<User> _users;

        public UserRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _users = bookStoreContext.Users;
        }

        public async Task AddAsync(User entity)
        {
            await _users.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var user =await _users.FindAsync(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.ToListAsync();
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
            if(await _users.FirstOrDefaultAsync(u=> u.UserId ==  entity.UserId) !=null)
                 _users.Update(entity);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email) ;
        }

        public async Task<User> GetByRefreshToken(string refreshToken)
        {
            return await _users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.Now);
        }
    }
}
