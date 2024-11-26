using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult> Get(int? id = null, string? name = null,bool? isDel =null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _supplierService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name) || isDel.HasValue)
            {
                result = await _supplierService.GetByFilterAsync(name, isDel);
            }
            else result = await _supplierService.GetAll(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPut]
        public async Task<IActionResult> Put(SupplierDto supplier)
        {
            var result = await _supplierService.Update(supplier);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpPut("changestatus")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var rs = await _supplierService.ChangeStatus(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPost]
        public async Task<ActionResult> PostSupplier(SupplierDto supplier)
        {
            var rs = await _supplierService.Add(supplier);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/Suppliers?id=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var rs = await _supplierService.Delete(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
