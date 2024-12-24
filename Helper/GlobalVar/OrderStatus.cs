using System.Runtime.Serialization;
namespace NhaSachDaiThang_BE_API.Helper.GlobalVar
{


    public static class OrderStatus
    {
        public const string OnlinePaymentPending = "Chờ thanh toán online";
        public const string Pending = "Chờ xác nhận";         
        public const string Confirmed = "Đã xác nhận";     
        public const string Processing = "Đang xử lý";      
        public const string Shipping = "Đang giao";            
        public const string DeliveredToCustomer = "Đã giao đến"; 
        public const string Cancelled = "Đã hủy";              
        public const string Returned = "Đã trả lại";
        public const string ReturnExchange = "Đổi trả";
        public const string Done = "Hoàn tất";
    }

}
