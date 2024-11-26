using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Helper
{
    public static class ServiceResultFactory
    {
        public static ServiceResult Create(int statusCode, string? message = null,string? errMessage = null, bool success = false, object data = null)
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                ApiResult = new ApiResult
                {
                    Success = success,
                    Message = message,
                    ErrMessage = errMessage,
                    Data = data
                }
            };
        }

        // 2xx Success
        public static ServiceResult Ok(string message = "Thành công", object data = null)
        {
            return Create(200, message,null, true, data);
        }

        public static ServiceResult Created(string message = "Đã tạo thành công", object data = null)
        {
            return Create(201, message,null, true, data);
        }

        public static ServiceResult Accepted(string message = "Yêu cầu đã được chấp nhận", object data = null)
        {
            return Create(202, message,null, true, data);
        }

        public static ServiceResult NoContent(string errMessage = "Không có nội dung")
        {
            return Create(204, null,errMessage, true);
        }

        // 4xx Client Errors
        public static ServiceResult BadRequest(string errMessage = "Yêu cầu không hợp lệ")
        {
            return Create(400,null, errMessage, false);
        }

        public static ServiceResult Unauthorized(string errMessage = "Không có quyền truy cập")
        {
            return Create(401, null, errMessage, false);
        }

        public static ServiceResult Forbidden(string errMessage = "Không được phép truy cập")
        {
            return Create(403,null,errMessage, false);
        }

        public static ServiceResult NotFound(string errMessage = "Không tìm thấy tài nguyên")
        {
            return Create(404, null, errMessage, false);
        }

        public static ServiceResult MethodNotAllowed(string errMessage = "Phương thức không được phép")
        {
            return Create(405, null, errMessage, false);
        }

        public static ServiceResult Conflict(string errMessage = "Xung đột dữ liệu")
        {
            return Create(409, null, errMessage, false);
        }

        public static ServiceResult UnprocessableEntity(string errMessage = "Không thể xử lý thực thể")
        {
            return Create(422, null, errMessage, false);
        }

        public static ServiceResult TooManyRequests(string errMessage = "Yêu cầu quá nhiều")
        {
            return Create(429, null, errMessage, false);
        }

        // 5xx Server Errors
        public static ServiceResult InternalServerError(string errMessage = "Lỗi máy chủ")
        {
            return Create(500, null, errMessage, false);
        }

        public static ServiceResult NotImplemented(string errMessage = "Chức năng chưa được triển khai")
        {
            return Create(501,null, errMessage, false);
        }

        public static ServiceResult BadGateway(string errMessage = "Cổng kết nối không hợp lệ")
        {
            return Create(502,null, errMessage, false);
        }

        public static ServiceResult ServiceUnavailable(string errMessage = "Dịch vụ không khả dụng")
        {
            return Create(503, null, errMessage, false);
        }

        public static ServiceResult GatewayTimeout(string errMessage = "Quá thời gian kết nối")
        {
            return Create(504, null, errMessage, false);
        }
    }


}
