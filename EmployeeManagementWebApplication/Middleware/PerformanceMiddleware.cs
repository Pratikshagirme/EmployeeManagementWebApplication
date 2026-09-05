using System.Diagnostics;

namespace EmployeeManagementWebApplication.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation("Request {Methode}{Path} took {ElapsedMillisecond} ms",context.Request.Method,context.Request.Path,stopwatch.ElapsedMilliseconds);

        }
    }
}
