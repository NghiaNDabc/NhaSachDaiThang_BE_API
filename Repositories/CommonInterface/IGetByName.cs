namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface IGetByName< T> where T : class
    {
        Task<IEnumerable<T>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null);
    }
}
