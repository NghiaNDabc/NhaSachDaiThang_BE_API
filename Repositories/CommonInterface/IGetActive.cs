using MimeKit.Tnef;

namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface IGetActive<T> where T : class
    {
        Task<IEnumerable<T>> GetAllActiveAsync();
        Task<T> GetActiveByIdAsync(int id);
        Task<IEnumerable<T>> GetActiveByNameAsync(string name);
    }
}
