using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rs = await  _userService.GetAll();

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpGet("Customer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var rs = await _userService.GetAll(AccountType.Customer);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpGet("Employee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var rs = await _userService.GetAll(AccountType.Employee); 

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpGet("Admin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var rs = await _userService.GetAll(AccountType.Admin);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpPost]
        public async Task<IActionResult> PostUser([FromForm]UserDTO userDTO, [FromForm]  List<IFormFile> formFiles)
        {
            var rs = await _userService.Add(userDTO, formFiles[0]);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpPut]
        public async Task<IActionResult> PutUser([FromForm] UserDTO userDTO, [FromForm] List<IFormFile> formFiles)
        {
            var rs = await _userService.Update(userDTO, formFiles[0]);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var rs = await _userService.Delete(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var rs = await _userService.SoftDelete(id); 

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
