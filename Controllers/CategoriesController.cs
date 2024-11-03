
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        [SwaggerOperation(Summary = "Lấy danh sách tất cả các danh mục", Tags = new[] { "Categories" })]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        public async Task<ActionResult> GetCategories(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _categoryService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _categoryService.GetByNameAsync(name);
            }
            else result = await _categoryService.GetAll(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);


        }
        [SwaggerOperation(Summary = "Lấy danh sách tất cả các danh mục đang được active", Tags = new[] { "Categories" })]
        [HttpGet("active")]
        // public async Task<ActionResult<IEnumerable<Category>>> GetActiveCategories()
        public async Task<ActionResult> GetActiveCategories(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _categoryService.GetActiveById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _categoryService.GetActiveByName(name);
            }
            else result = await _categoryService.GetAllActive(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        //// GET: api/Categories/5
        //[SwaggerOperation(Summary = "Lấy danh sách tất cả các danh mục theo id", Tags = new[] { "Categories" })]
        //[HttpGet("{id}")]
        ////public async Task<ActionResult<Category>> GetCategory(int id)
        //public async Task<ActionResult> GetCategory(int id)
        //{
        //    var result = await _categoryService.GetById(id);

        //    return StatusCode(result.StatusCode, result.ApiResult);
        //}

        [SwaggerOperation(Summary = "Sửa danh mục", Tags = new[] { "Categories" })]
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        public async Task<IActionResult> PutCategory(CategoryDto category)
        {
            var result = await _categoryService.Update(category);

            return StatusCode(result.StatusCode, result.ApiResult);

        }

        // POST: api/Categories
        [SwaggerOperation(Summary = "Thêm mới danh mục", Tags = new[] { "Categories" })]
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto category)
        {
            var rs = await _categoryService.Add(category);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/Categories/5
        [SwaggerOperation(Summary = "Xóa mềm danh mục", Tags = new[] { "Categories" })]
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactiveCategory(int id)
        {
            var rs = await _categoryService.SoftDelete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [SwaggerOperation(Summary = "Xóa cứng", Tags = new[] { "Categories" })]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDeleteCategory(int id)
        {
            var rs = await _categoryService.Delete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        //private bool CategoryExists(int id)
        //{
        //    return _context.Categories.Any(e => e.CategoryId == id);
        //}
    }
}
