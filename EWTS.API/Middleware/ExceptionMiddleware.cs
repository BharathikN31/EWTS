using System.Net;
using System.Text.Json;

namespace EWTS.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Map known business exceptions to appropriate status codes
            var statusCode = ex.Message switch
            {
                var m when m.Contains("not found", StringComparison.OrdinalIgnoreCase)
                    => HttpStatusCode.NotFound,
                var m when m.Contains("not allowed", StringComparison.OrdinalIgnoreCase)
                    || m.Contains("Only Manager", StringComparison.OrdinalIgnoreCase)
                    => HttpStatusCode.Forbidden,
                var m when m.Contains("already exists", StringComparison.OrdinalIgnoreCase)
                    || m.Contains("Invalid email", StringComparison.OrdinalIgnoreCase)
                    || m.Contains("must be completed", StringComparison.OrdinalIgnoreCase)
                    => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = (int)statusCode,
                message = ex.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}