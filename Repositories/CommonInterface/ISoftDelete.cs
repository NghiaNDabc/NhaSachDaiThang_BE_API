namespace NhaSachDaiThang_BE_API.Repositories.CommonInterface
{
    public interface ISoftDelete
    {
        Task SoftDelete(int id);
    }
}
