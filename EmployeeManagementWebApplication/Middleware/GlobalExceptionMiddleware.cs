using EmployeeManagementWebApplication.Model;
using System.Net;
using System.Text.Json;


namespace EmployeeManagementWebApplication.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured. Request:{method} {Path}", context.Request.Method, context.Request.Path);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                CommonResponse response = new CommonResponse
                {
                    StatusCode = 500,
                    Message = "Something went wrong, Please try again later",
                    Data = null
                };
                string jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);

            }
        }
    }
}
