using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyBy { get; set; }

        public  List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();

    }
}
