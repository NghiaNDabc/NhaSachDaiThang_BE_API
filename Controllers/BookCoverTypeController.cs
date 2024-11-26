using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookCoverTypeController : ControllerBase
    {
        private readonly IBookCoverTypeService _bookCoverTypeService;

        public BookCoverTypeController(IBookCoverTypeService bookCoverTypeService)
        {
            _bookCoverTypeService = bookCoverTypeService;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult> Get(int? id = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _bookCoverTypeService.GetByIdAsync(id.Value);
            }
            else result = await _bookCoverTypeService.GetAllAsync();
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPut]
        public async Task<IActionResult> PutSupplier(BookCoverTypeDto lg)
        {
            var result = await _bookCoverTypeService.UpdateAsync(lg);
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPost]
        public async Task<ActionResult> PostSupplier(BookCoverTypeDto lg)
        {
            var rs = await _bookCoverTypeService.AddAsync(lg);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/Suppliers?id=1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var rs = await _bookCoverTypeService.DeleteAsync(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
