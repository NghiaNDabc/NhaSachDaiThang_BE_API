using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult> Get(int? id = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _languageService.GetByIdAsync(id.Value);
            }
            else result = await _languageService.GetAllAsync();
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPut]
        public async Task<IActionResult> PutSupplier(LanguageDto lg)
        {
            var result = await _languageService.UpdateAsync(lg);
            return StatusCode(result.StatusCode, result.ApiResult);
        }


        [HttpPost]
        public async Task<ActionResult> PostSupplier(LanguageDto lg)
        {
            var rs = await _languageService.AddAsync(lg);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        // DELETE: api/Suppliers?id=1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var rs = await _languageService.DeleteAsync(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
