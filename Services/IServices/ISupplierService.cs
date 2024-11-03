using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.CommonInterface;
using NhaSachDaiThang_BE_API.Services.CommonInterface;

namespace NhaSachDaiThang_BE_API.Services.IServices
{
    public interface ISupplierService:ICrudService<SupplierDto>, IActiveService, IGetByNameService
    {
    }
}
