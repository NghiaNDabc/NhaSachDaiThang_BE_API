using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetBook(int? id = null,int?categoryId=null,string? name=null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
                result = await _bookService.GetById(id.Value);
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _bookService.GetByNameAndCategoryIdAsync(categoryId,name, pageNumber, pageSize);
            }
            else
                result = await _bookService.GetAll(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("count")]
        public async Task<IActionResult> Sum()
        {
            ServiceResult result = await _bookService.Count();
            return StatusCode(result.StatusCode, result.ApiResult);
            
            
        }
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive(int? id = null, int? categoryId = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
                result = await _bookService.GetActiveById(id.Value);
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _bookService.GetActiveByNameAndCategoryIdAsync(categoryId,name, pageNumber, pageSize);
            }
            else
                result = await _bookService.GetAllActiveAsync();
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult>> PostBook([FromForm] BookDto bookDto, [FromForm] List<IFormFile> imageFiles)
        {
            var result = await _bookService.AddAsync(bookDto, imageFiles);
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPut("changeStatus")]
        public async Task<IActionResult> DeactiveCategory(int id)
        {
            var rs = await _bookService.SoftDelete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDeleteCategory(int id)
        {
            var rs = await _bookService.Delete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPut]
        public async Task<IActionResult> PutBook([FromForm] BookDto book, [FromForm] List<IFormFile> formFiles)
        {
            var rs = await _bookService.Update(book, formFiles);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
