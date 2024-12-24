namespace NhaSachDaiThang_BE_API.Helper
{
    public class UrlHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                throw new InvalidOperationException("HttpContext không tồn tại.");

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return baseUrl;
        }
    }
}
