namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface ICrudRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void  Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }

}
