using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
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

        [HttpGet()]
        //[Authorize(Roles = "Admin,Employee")]

        public async Task<IActionResult> GetBook(int? id = null,
            int? categoryId = null,
            string? categoryName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? bookName = null,
            int? minQuatity = null,
            int? maxQuanlity = null,
            bool? isPromotion = null,
            int? languageId = null,
            int? bookCoverTypeId = null,
            int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
                result = await _bookService.GetByIdAsync(id.Value);
            else if (categoryId.HasValue ||
              !string.IsNullOrEmpty(categoryName) ||
              minPrice.HasValue ||
              maxPrice.HasValue ||
              maxPrice.HasValue ||
              minQuatity.HasValue ||
              maxQuanlity.HasValue ||
              isPromotion.HasValue ||
              languageId.HasValue ||
              bookCoverTypeId.HasValue ||
              !string.IsNullOrEmpty(bookName))
            {

                result = await _bookService.GetByFilterAsync(categoryId,
                                                            categoryName,
                                                            minPrice,
                                                            maxPrice,
                                                            bookName,
                                                            minQuatity,
                                                            maxQuanlity,
                                                            isPromotion,
                                                            languageId,
                                                            bookCoverTypeId,
                                                            pageNumber,
                                                            pageSize);
            }
            else
                result = await _bookService.GetAllAsync(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("count")]
        public async Task<IActionResult> Sum()
        {
            ServiceResult result = await _bookService.CountAsync();
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive(
            int? id = null,
            int? categoryId = null,
            string? categoryName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? bookName = null,
            int? minQuatity = null,
            int? maxQuanlity = null,
            bool? isPromotion = null,
            int? languageId = null,
            int? bookCoverTypeId = null,
            int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
                result = await _bookService.GetActiveByIdAsync(id.Value);
            else if (categoryId.HasValue ||
               !string.IsNullOrEmpty(categoryName) ||
               minPrice.HasValue ||
               maxPrice.HasValue ||
               !string.IsNullOrEmpty(bookName))
            {
                result = await _bookService.GetActiveByFilterAsync(
                    categoryId,
                                                            categoryName,
                                                            minPrice,
                                                            maxPrice,
                                                            bookName,
                                                            minQuatity,
                                                            maxQuanlity,
                                                            isPromotion,
                                                            languageId,
                                                            bookCoverTypeId,
                                                            pageNumber,
                                                            pageSize);
            }
            else
                result = await _bookService.GetAllActiveAsync();
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("newBook")]
        public async Task<IActionResult> GetNewBook()
        {
            ServiceResult result;
            result = await _bookService.GetNewBooks();
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<ServiceResult>> PostBook([FromForm] BookDto bookDto, [FromForm] List<IFormFile> imageFiles)
        {
            var result = await _bookService.AddAsync(bookDto, imageFiles);
            return StatusCode(result.StatusCode, result.ApiResult);
        }

        [HttpPut("changeStatus/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeactiveCategory(int id)
        {
            var rs = await _bookService.ChangStatusAsync(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> HardDeleteCategory(int id)
        {
            var rs = await _bookService.DeleteAsync(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> PutBook([FromForm] BookDto book, [FromForm] List<IFormFile> imageFiles)
        {
            var rs = await _bookService.UpdateAync(book, imageFiles);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
