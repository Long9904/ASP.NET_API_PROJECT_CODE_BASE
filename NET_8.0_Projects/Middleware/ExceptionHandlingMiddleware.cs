using System.Text.Json;
using Application.Exceptions;

namespace NET_8._0_Projects.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception has occurred.");

                context.Response.ContentType = "application/json";

                var message = "An internal server error occurred.";
                var statusCode = StatusCodes.Status500InternalServerError;

                if (ex is BaseException baseException)
                {
                    statusCode = baseException.StatusCode;
                    message = baseException.Message;
                }

                var response = new
                {
                    StatusCode = statusCode,
                    IsSuccess = false,
                    Message = message,
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
