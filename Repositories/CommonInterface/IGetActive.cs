using MimeKit.Tnef;

namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface IGetActive<T> where T : class
    {
        Task<IEnumerable<T>> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null);
        Task<T> GetActiveByIdAsync(int id);
        Task<IEnumerable<T>> GetActiveByNameAsync(string name, int? pageNumber = null, int? pageSize = null);
    }
}
