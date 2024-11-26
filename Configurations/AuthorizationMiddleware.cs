using System.Net;

namespace NhaSachDaiThang_BE_API.Configurations
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorizationMiddleware> _logger;
        public AuthorizationMiddleware(RequestDelegate requestDelegate, ILogger<AuthorizationMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>() != null
                && !context.Request.Headers.ContainsKey("Authorization")
                ){
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var unauthorization = new
                {
                    ErrMessage = "Authorization token is missing."

                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(unauthorization);
                return;
            }
            //try
            //{
                await _next(context);
            //}
            //catch (Exception ex) {
            //    _logger.LogError(ex, "An error occurred in Authorization Middleware.");

            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //    await context.Response.WriteAsJsonAsync(new { 
            //        Exception = ex.ToString(),
            //        ErrMessage = "An error occurred while processing your request." });
            //}
        }

    }
}
