namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface IGetByName< T> where T : class
    {
        Task<T> GetByNameAsync(string name);
    }
}
