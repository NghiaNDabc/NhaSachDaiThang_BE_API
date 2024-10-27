using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        [Authorize (Roles ="Admin")]
        [HttpGet("protected")]
        public IActionResult GetProtectedData()
        {
            return Ok("This is protected data.");
        }
        //[Authorize (Roles ="Admin")]
        //[HttpGet("admin")]
        //public IActionResult GetAdmin()
        //{
        //    return Ok("This is protected data.");
        //}
    }
}
