using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NET_8._0_Projects.Common;

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
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", context.TraceIdentifier);

                var problem = new ApiProblemDetails
                {
                    Type = "https://errors.myapp.com/internal-server-error",
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = "An unexpected error occurred",
                    Instance = context.Request.Path,
                    ErrorCode = "INTERNAL_SERVER_ERROR",
                    TraceId = context.TraceIdentifier
                };

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsJsonAsync(problem);
            }
        }


        private static async Task HandleValidationException(
        HttpContext context,
        ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var problem = new ValidationProblemDetails(errors)
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest
            };

            context.Response.StatusCode = problem.Status.Value;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
