using Microsoft.EntityFrameworkCore;
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

        public void Add(User entity)
        {
            _users.AddAsync(entity);
            _bookStoreContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _users.Find(id);
            if (user != null)
            {
                _users.Remove(user);
                _bookStoreContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            var user = _users.Where(u => u.UserId == id).FirstOrDefault();
            return user;
        }

        public IEnumerable<User> GetPagedAsync(int pageNumber, int pageSize)
        {
            return _users.OrderBy(e => e.UserId).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        public void Update(User entity)
        {
            _users.Update(entity);
            _bookStoreContext.SaveChangesAsync();
        }

        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
    }
}
