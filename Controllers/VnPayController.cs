using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;


namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        public VnPayController(IVnPayService vnPayService, IOrderService orderService, IConfiguration configuration, IOrderRepository orderRepository)
        {
            _vnPayService = vnPayService;
            _orderService = orderService;
            _configuration = configuration;
            _orderRepository = orderRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl(OrderDto model)
        {
            var rs = await _orderService.AddAsync(model);
            if (rs.StatusCode != 201) return StatusCode(rs.StatusCode, rs.ApiResult);
            dynamic data = rs.ApiResult.Data;
            model.OrderId = int.Parse(data.orderId.ToString());

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Ok(new
            {
                success = true,
                message = "Tạo URL thanh toán thành công.",
                paymentUrl = url
            });
        }
        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreatePaymentUrl(int orderId)
        {
            var  model = await _orderRepository.GetByIdAsync(orderId);

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Ok(new
            {
                success = true,
                message = "Tạo URL thanh toán thành công.",
                paymentUrl = url
            });
        }
        [HttpGet("PaymentCallBack")]
        public async Task<IActionResult> PaymentCallBack()
        {
            IQueryCollection collections = Request.Query;
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            if (response.Success == false)
            {
               //await _orderService.DeleteAsync(int.Parse(response.OrderId));
            }
            else
            {
                _orderService.UpdateStauaAsync(int.Parse(response.OrderId), OrderStatus.Pending);

            }

            var queryParams = new Dictionary<string, string>
            {
                { "orderId", response.OrderId },
                { "success", response.Success.ToString() },
                { "transactionId", response.TransactionId }
            };

            // Tạo URL với query string
            var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={kv.Value}"));

            return Redirect($"http://localhost:3000/resultcheckout?{queryString}");

        }
    }
}
