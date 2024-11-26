using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SupplierBooksController : ControllerBase
    {
        private readonly ISupplierBookService _supplierBookService;

        public SupplierBooksController(ISupplierBookService supplierBookService)
        {
            _supplierBookService = supplierBookService;
        }

        // GET: api/SupplierBooks
        [HttpGet]
        public async Task<ActionResult> GetSupplierBook(int?supplierBookId=null,int? supplierId = null,  string? supplierName = null, string? bookname = null, DateTime? minSupplyDate = null, DateTime? maxSupplyDate = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult serviceResult;
            if (supplierId.HasValue)
            {
                serviceResult = await _supplierBookService.GetBySuppierIdAsync(supplierId.Value);
            }
            else if (!string.IsNullOrEmpty(supplierName) || !string.IsNullOrEmpty(bookname))
            {
                serviceResult = await _supplierBookService.GetByFilterAsync(supplierId, null, bookname, supplierName, minSupplyDate, maxSupplyDate, pageNumber, pageSize);
            }
            else
                serviceResult = await _supplierBookService.GetAll(pageNumber, pageSize);
            return StatusCode(serviceResult.StatusCode, serviceResult.ApiResult);
        }



        //[HttpPut]
        //public async Task<IActionResult> PutSupplierBook(SupplierBookDto supplierBook)
        //{
        //    var rs = await _supplierBookService.Update(supplierBook);
        //    return StatusCode(rs.StatusCode, rs.ApiResult);
        //}

        // POST: api/SupplierBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierBook>> PostSupplierBook(IEnumerable<SupplierBookDto> supplierBook)
        {
            var rs = await _supplierBookService.AddRangeAsync(supplierBook);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/SupplierBooks
        [HttpDelete]
        public async Task<IActionResult> DeleteSupplierBook(int id)
        {
            var rs = await _supplierBookService.Delete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

    }
}
