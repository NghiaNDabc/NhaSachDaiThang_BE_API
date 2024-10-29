namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface IGetActive<T> where T : class
    {
        Task<IEnumerable<T>> GetActiveAsync();
    }
}
