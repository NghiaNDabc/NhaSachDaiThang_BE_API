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
        BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(int? id = null)
        {
            ServiceResult result;
            if (id.HasValue)
                result = await _bookService.GetById(id.Value);
            else
                result = await _bookService.GetAll();
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive(int? id = null)
        {
            ServiceResult result;
            if (id.HasValue)
                result = await _bookService.GetActiveById(id.Value);
            else
                result = await _bookService.GetAllActive();
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult>> PostBook([FromForm] BookDto bookDto, [FromForm] List<IFormFile> imageFiles)
        {
            var result = await _bookService.Add(bookDto, imageFiles);
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var rs = await _bookService.SoftDelete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpDelete("hard/{id}")]
        public async Task<IActionResult> HardDeleteCategory(int id)
        {
            var rs = await _bookService.Delete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPut]
        public async Task<IActionResult> PutBook(BookDto book)
        {
            throw new NotImplementedException();
        }
    }
}
