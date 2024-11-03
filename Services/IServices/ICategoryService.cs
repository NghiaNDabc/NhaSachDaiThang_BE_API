using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ICategoryService : ICrudService<CategoryDto>, IActiveService, IGetByNameService
    {
    }
}
