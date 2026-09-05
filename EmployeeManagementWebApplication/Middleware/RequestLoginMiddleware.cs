public class RequestLoginMiddleware : IMiddleware
{
    private readonly ILogger<RequestLoginMiddleware> _logger;

    public RequestLoginMiddleware(
        ILogger<RequestLoginMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        _logger.LogInformation(
            "Request started: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        await next(context);

        _logger.LogInformation(
            "Request completed: {StatusCode}",
            context.Response.StatusCode);
    }
}