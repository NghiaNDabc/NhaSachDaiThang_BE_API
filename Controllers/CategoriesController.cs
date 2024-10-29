
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [Authorize(Roles ="Admin,Employee")]
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        public async Task<ActionResult> GetCategories()
        {
            var result = await _categoryService.GetAll();
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("active")]
       // public async Task<ActionResult<IEnumerable<Category>>> GetActiveCategories()
        public async Task<ActionResult> GetActiveCategories()
        {
            var result = await _categoryService.GetAllActive();
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        // GET: api/Categories/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Category>> GetCategory(int id)
        public async Task<ActionResult> GetCategory(int id)
        {
            var result = await _categoryService.GetById(id);

            return  StatusCode(result.StatusCode, result.ApiResult);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        public async Task<IActionResult> PutCategory(Category category)
        {
            var result = await _categoryService.Update(category);

            return StatusCode(result.StatusCode, result.ApiResult);

        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var rs = await _categoryService.Add(category);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/Categories/5
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var rs = await _categoryService.SoftDelete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("hard/{id}")]
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
