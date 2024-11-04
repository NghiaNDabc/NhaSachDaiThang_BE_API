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
    [Route("api/[controller]")]
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
        public async Task<ActionResult> GetSupplierBook(int? supplierId = null, int? bookId = null, string? supplierName = null, string? bookname = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult serviceResult;
            if (supplierId.HasValue || bookId.HasValue)
            {
                serviceResult = await _supplierBookService.GetByIdAsync(bookId.Value, supplierId.Value, pageNumber, pageSize);
            }
            else if (!string.IsNullOrEmpty(supplierName) || !string.IsNullOrEmpty(bookname) ){ serviceResult = await _supplierBookService.GetByNameAsync(bookname, supplierName, pageNumber, pageSize); }
            else
                serviceResult = await _supplierBookService.GetAll(pageNumber, pageSize);
            return StatusCode(serviceResult.StatusCode, serviceResult.ApiResult);
        }



        [HttpPut]
        public async Task<IActionResult> PutSupplierBook( SupplierBookDto supplierBook)
        {
            var rs = await _supplierBookService.Update(supplierBook);
            return StatusCode(rs.StatusCode, rs.ApiResult); 
        }

        // POST: api/SupplierBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierBook>> PostSupplierBook(SupplierBookDto supplierBook)
        {
            var rs = await _supplierBookService.Add(supplierBook);
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
