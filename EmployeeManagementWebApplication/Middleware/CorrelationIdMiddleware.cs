using Serilog.Context;

namespace EmployeeManagementWebApplication.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId = Guid.NewGuid().ToString();
            context.Items["CorrelationId"] = correlationId;
            context.Response.Headers["X-CorrelationID-Id"] = correlationId;
            using (LogContext.PushProperty("CorrelationId",correlationId))
            {
                await _next(context);
            }
            
        }
    }
}
