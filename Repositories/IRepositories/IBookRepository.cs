using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;

namespace NhaSachDaiThang_BE_API.Repositories.IRepositories
{
    public interface IBookRepository //: ICrudRepository<Book>, IGetActive<Book>, IGetByName<Book>, ISoftDelete
    {
        Task AddAsync(Book entity);
        Task DeleteAsync(int id);
        Task <int> CountActive();
        Task <int> CountDeactive();
        Task<int> CountByFillterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, int? languageId = null, int? bookCoverTypeId = null, bool? IsDel = null);
        Task<IEnumerable<Book>> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null);
        Task<Book> GetActiveByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        Task<Book> GetByIdAsync(int id);
        Task SoftDeleteAsync(int id);
        Task UpdateAsync(Book entity);
        Task<IEnumerable<Book>> GetByFilterAsync(int? categoryid = null,string?categoryName =null,decimal? minPrice=null, decimal? maxPrice=null, string? bookName = null,int?minQuality=null, int? maxQuanlity = null,bool? isPromotion = null,int ?languageId=null,int? bookCoverTypeId=null, int? pageNumber = null, int? pageSize = null);
        Task<IEnumerable<Book>> GetActiveByFilterAsync(int? categoryid = null, string? categoryName = null, decimal? minPrice = null, decimal? maxPrice = null, string? bookName = null, int? minQuality = null, int? maxQuanlity = null, bool? isPromotion = null, int? languageId = null, int? bookCoverTypeId = null, int? pageNumber = null, int? pageSize = null);
    }
}
