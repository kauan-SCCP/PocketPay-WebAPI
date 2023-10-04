

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var userAgent = context.Request.Headers.UserAgent;

        _logger.LogInformation("[{Timestamp}] {HttpMethod} {Path} - {UserAgent}", DateTime.UtcNow, method, path, userAgent);

        await _next(context);
    }
}