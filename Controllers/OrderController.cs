using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;
using System.Security.Claims;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int? orderId = null, DateTime? minOrderDate = null, DateTime? maxOrderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? userId = null, string? phoneNumber = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult rs;
            if (orderId.HasValue)
            {
                rs = await _orderService.GetByIdAsync(orderId.Value);
            }
            else
            {
                rs = await _orderService.GetFilteredAsync(minOrderDate, maxOrderDate, deliverdDate, customerName, status, userId, phoneNumber, pageNumber, pageSize);
            }
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpPut]
        public async Task<IActionResult> Put(OrderDto orderdto)
        {
            var rs = await _orderService.UpdateAsync(orderdto);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("changestatus")]
        public async Task<IActionResult> ChangStatus(int orderId, string status)
        {
            var rs = await _orderService.UpdateStauaAsync(null,orderId, status);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [Authorize]
        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> Cancel(int orderId)
        {

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier); 
            var userId = int.Parse(userIdClaim.Value);
            var rs = await _orderService.UpdateStauaAsync(userId, orderId, OrderStatus.Cancelled);
            return StatusCode(rs.StatusCode);
        }
        [HttpPost]
        public async Task<IActionResult> Post(OrderDto orderdto)
        {
            var rs = await _orderService.AddAsync(orderdto);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

    }
}
