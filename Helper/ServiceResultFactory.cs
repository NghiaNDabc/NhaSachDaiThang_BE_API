using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Helper
{
    public static class ServiceResultFactory
    {
        public static ServiceResult Create(int statusCode, string message = "", bool success = false, object data = null)
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                ApiResult = new ApiResult
                {
                    Success = success,
                    Message = message,
                    Data = data
                }
            };
        }

        // 2xx Success
        public static ServiceResult Ok(string message = "Thành công", object data = null)
        {
            return Create(200, message, true, data);
        }

        public static ServiceResult Created(string message = "Đã tạo thành công", object data = null)
        {
            return Create(201, message, true, data);
        }

        public static ServiceResult Accepted(string message = "Yêu cầu đã được chấp nhận", object data = null)
        {
            return Create(202, message, true, data);
        }

        public static ServiceResult NoContent(string message = "Không có nội dung")
        {
            return Create(204, message, true);
        }

        // 4xx Client Errors
        public static ServiceResult BadRequest(string message = "Yêu cầu không hợp lệ")
        {
            return Create(400, message, false);
        }

        public static ServiceResult Unauthorized(string message = "Không có quyền truy cập")
        {
            return Create(401, message, false);
        }

        public static ServiceResult Forbidden(string message = "Không được phép truy cập")
        {
            return Create(403, message, false);
        }

        public static ServiceResult NotFound(string message = "Không tìm thấy tài nguyên")
        {
            return Create(404, message, false);
        }

        public static ServiceResult MethodNotAllowed(string message = "Phương thức không được phép")
        {
            return Create(405, message, false);
        }

        public static ServiceResult Conflict(string message = "Xung đột dữ liệu")
        {
            return Create(409, message, false);
        }

        public static ServiceResult UnprocessableEntity(string message = "Không thể xử lý thực thể")
        {
            return Create(422, message, false);
        }

        public static ServiceResult TooManyRequests(string message = "Yêu cầu quá nhiều")
        {
            return Create(429, message, false);
        }

        // 5xx Server Errors
        public static ServiceResult InternalServerError(string message = "Lỗi máy chủ")
        {
            return Create(500, message, false);
        }

        public static ServiceResult NotImplemented(string message = "Chức năng chưa được triển khai")
        {
            return Create(501, message, false);
        }

        public static ServiceResult BadGateway(string message = "Cổng kết nối không hợp lệ")
        {
            return Create(502, message, false);
        }

        public static ServiceResult ServiceUnavailable(string message = "Dịch vụ không khả dụng")
        {
            return Create(503, message, false);
        }

        public static ServiceResult GatewayTimeout(string message = "Quá thời gian kết nối")
        {
            return Create(504, message, false);
        }
    }


}
