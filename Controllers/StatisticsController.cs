using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly BookStoreContext _context;
        private readonly UrlHelper _urlHelper;
        public StatisticsController(BookStoreContext context, UrlHelper urlHelper)
        {
            _context = context;
            _urlHelper = urlHelper;
        }

        [HttpGet("topSelling")]

        public async Task<ActionResult> TopSelling(int? soLuong = null, DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null)
        {
            var baseUrl = _urlHelper.GetBaseUrl();
            IEnumerable<OrderDetail> query = _context.OrderDetail;
            if (ngayBatDau != null)
            {
                query = query.Where(od => od.Order.CreatedDate.Value.Date >= ngayBatDau);
            }
            if (ngayKetThuc != null)
            {
                query = query.Where(od => od.Order.CreatedDate.Value.Date <= ngayKetThuc);
            }
            var bookReport = query
           .Where(od => od.BookId != null && od.Quantity != null && od.Order.Status == "Hoàn tất") // Bỏ qua các bản ghi không hợp lệ
           .GroupBy(od => new { od.BookId, od.Book.Title, od.Book.MainImage, od.Book.Author })       // Nhóm theo BookId và lấy tên sách
           .Select(g => new
           {
               g.Key.BookId,
               g.Key.Title,
               MainImage = $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{g.Key.MainImage}",
               g.Key.Author,
               TotalQuantity = g.Sum(od => od.Quantity ?? 0)      // Tổng số lượng bán
           })
           .OrderByDescending(b => b.TotalQuantity);         // Sắp xếp giảm dần theo tổng số lượng
            if (bookReport.Any() && soLuong.HasValue)
            {
                bookReport.Take(soLuong.Value);
            }
            var rs = bookReport.ToList();
            var count = rs.Count();
            var apirs = new
            {
                Count = count,
                Data = rs
            };
            return Ok(apirs);
        }

        [HttpGet("revenue-by-date")]
        public IActionResult GetRevenueByDate(DateTime? ngayBatDau = null, DateTime? ngayKetThuc = null)
        {
            IEnumerable<Order> query = _context.Order.Where(o => o.Status == "Hoàn tất");
            if (ngayBatDau != null)
            {
                query = query.Where(od => od.CreatedDate.Value.Date >= ngayBatDau);
            }
            if (ngayKetThuc != null)
            {
                query = query.Where(od => od.CreatedDate.Value.Date <= ngayKetThuc);
            }

            var revenueData = query
                .GroupBy(o => o.OrderDate.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalAmount ?? 0)
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(new
            {
                Success = true,
                Data = revenueData
            });


        }
    }
}
