using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;

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
        public async Task<IActionResult> Get(int? orderId = null, DateTime? orderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? userId = null, string? phoneNumber = null, int? pageNumber = null, int? pageSize = null)
        {
            ServiceResult rs;
            if (orderId.HasValue)
            {
                rs = await _orderService.GetByIdAsync(orderId.Value);
            }
            else
            {
                rs = await _orderService.GetFilteredAsync(orderDate, deliverdDate, customerName, status,userId,phoneNumber, pageNumber, pageSize);
            }
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpPut]
        public async Task<IActionResult> Put(OrderDto orderdto)
        {
            var rs = await _orderService.UpdateAsync(orderdto);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }
        [HttpPut("changstatus")]
        public async Task<IActionResult> ChangStatus(int orderId, string status)
        {
            var rs = await _orderService.UpdateStauaAsync(orderId,status);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderDto orderdto)
        {
            var rs = await _orderService.AddAsync(orderdto);
            return StatusCode(rs.StatusCode, rs.ApiResult);
        }

    }
}
