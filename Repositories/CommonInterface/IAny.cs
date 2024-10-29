using System.Linq.Expressions;

namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface IAny<T> where T : class
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
