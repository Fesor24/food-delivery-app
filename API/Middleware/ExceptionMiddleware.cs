using System.Net;
using System.Text.Json;
using API.Response;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occurred. Details: {error}", ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";

                var exception = _env.IsDevelopment() ? new ApiResponse
                {
                    ErrorMessage = $"An error occurred. Details: {ex.StackTrace}",
                    ErrorResult = ex.StackTrace
                } : new ApiResponse { ErrorMessage = $"An error occurred. Details: {ex.StackTrace}" };

                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var serializedError = JsonSerializer.Serialize(exception, jsonOptions);

                await context.Response.WriteAsync(serializedError);
            }
        }
    }
}
