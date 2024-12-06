using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Services.IServices;
using System.Drawing.Printing;

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
        public async Task<IActionResult> GetAll(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _userService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _userService.GetByNameAsync(name);
            }
            else result = await _userService.GetAll(pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("Customer")]
        public async Task<IActionResult> GetAllCustomer(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _userService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _userService.GetByNameAsync(name, AccountType.Customer, pageNumber, pageSize);
            }
            else result = await _userService.GetAllAsync(AccountType.Customer, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("Employee")]
        public async Task<IActionResult> GetAllEmployee(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _userService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _userService.GetByNameAsync(name, AccountType.Employee, pageNumber, pageSize);
            }
            else result = await _userService.GetAllAsync(AccountType.Employee, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [HttpGet("Admin")]
        public async Task<IActionResult> GetAllAdmin(int? id = null, string? name = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult result;
            if (id.HasValue)
            {
                result = await _userService.GetById(id.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                result = await _userService.GetByNameAsync(name, AccountType.Admin, pageNumber, pageSize);
            }
            else result = await _userService.GetAllAsync(AccountType.Admin, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result.ApiResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromForm] UserDTO userDTO, [FromForm] List<IFormFile> imageFiles)
        {
            ServiceResult rs;
            if (imageFiles == null || imageFiles.Count == 0)
            {
                rs = await _userService.AddAsync(userDTO, null);
            }
            else
            {
                rs = await _userService.AddAsync(userDTO, imageFiles[0]);
            }

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPut("client")]
        public async Task<IActionResult> ClientPutUser(UserDTO userDTO)
        {
            ServiceResult rs;
            rs = await _userService.ClientUpdateAsync(userDTO);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpPut("client/changepassword")]
        public async Task<IActionResult> ClientChangPass(ChangePassDto userDTO)
        {
            ServiceResult rs;
            rs = await _userService.ClientChangePassAsync(userDTO);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> PutUser([FromForm] UserDTO userDTO, [FromForm] List<IFormFile> imageFiles)
        {
            ServiceResult rs;
            if (imageFiles == null || imageFiles.Count == 0)
            {
                rs = await _userService.UpdateAsync(userDTO, null);
            }
            else
            {
                rs = await _userService.UpdateAsync(userDTO, imageFiles[0]);
            }
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var rs = await _userService.Delete(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("changestatus/{id}")]
        public async Task<IActionResult> ChangStatus(int id)
        {
            var rs = await _userService.ChangStatus(id);

            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
    }
}
