using System.Runtime.Serialization;
namespace NhaSachDaiThang_BE_API.Helper.GlobalVar
{


    public static class OrderStatus
    {
        public const string OnlinePaymentPending = "Chờ thanh toán online";
        public const string Pending = "Chờ xử lý";         // Đơn hàng đang chờ xử lý
        public const string Confirmed = "Đã xác nhận";     // Đơn hàng đã được xác nhận
        public const string Processing = "Đang xử lý";       // Đơn hàng đang được xử lý
        public const string Shipping = "Đang giao";            // Đơn hàng đã được giao
        public const string DeliveredToCustomer = "Đã giao đến"; // Đơn hàng đã được giao đến khách hàng
        public const string Cancelled = "Đã hủy";              // Đơn hàng đã bị hủy
        public const string Returned = "Đã trả lại";
        public const string ReturnExchange = "Đổi trả";
        public const string Done = "Hoàn tất";
    }

}
