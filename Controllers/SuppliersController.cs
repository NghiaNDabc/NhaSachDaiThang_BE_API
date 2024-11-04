using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult> GetSupplier(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _supplierService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _supplierService.GetByNameAsync(name);
            }
            else result = await _supplierService.GetAll(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPut]
        public async Task<IActionResult> PutSupplier(SupplierDto supplier)
        {
            var result = await _supplierService.Update(supplier);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var rs = await _supplierService.SoftDelete(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPost]
        public async Task<ActionResult> PostSupplier(SupplierDto supplier)
        {
            var rs = await _supplierService.Add(supplier);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/Suppliers?id=1
        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var rs = await _supplierService.Delete(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        //private bool SupplierExists(int id)
        //{
        //    return _context.Supplier.Any(e => e.SupplierId == id);
        //}
    }
}
