

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
        var statusCode = context.Response.StatusCode;
        
        _logger.LogInformation("[{Timestamp}] ({StatusCode}) {HttpMethod} {Path} - {UserAgent}", DateTime.UtcNow, statusCode, method, path, userAgent);

        await _next(context);
    }
}