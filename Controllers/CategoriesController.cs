﻿
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
        [Authorize(Roles ="Admin,Employee")]
        [HttpGet]
        [SwaggerOperation(Summary = "Lấy danh sách tất cả các danh mục", Tags = new[] { "Categories" })]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        public async Task<ActionResult> GetCategories()
        {
            var result = await _categoryService.GetAll();
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [SwaggerOperation(Summary = "Lấy danh sách tất cả các danh mục đang được active", Tags = new[] { "Categories" })]
        [HttpGet("active")]
       // public async Task<ActionResult<IEnumerable<Category>>> GetActiveCategories()
        public async Task<ActionResult> GetActiveCategories()
        {
            var result = await _categoryService.GetAllActive();
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        // GET: api/Categories/5
        [SwaggerOperation(Summary = "Lấy danh sách tất cả các danh mục theo id", Tags = new[] { "Categories" })]
        [HttpGet("{id}")]
        //public async Task<ActionResult<Category>> GetCategory(int id)
        public async Task<ActionResult> GetCategory(int id)
        {
            var result = await _categoryService.GetById(id);

            return  StatusCode(result.StatusCode, result.ApiResult);
        }

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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var rs = await _categoryService.SoftDelete(id);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [SwaggerOperation(Summary = "Xóa cứng", Tags = new[] { "Categories" })]
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
