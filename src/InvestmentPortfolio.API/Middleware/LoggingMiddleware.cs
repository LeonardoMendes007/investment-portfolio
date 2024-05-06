namespace InvestmentPortfolio.API.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] Received request: {context.Request.Method} {context.Request.Path}");

        await _next(context);

        _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] Finished processing request: {context.Request.Method} {context.Request.Path} => {context.Response.StatusCode}");
    }
}
