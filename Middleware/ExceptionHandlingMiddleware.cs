using GameClubAndEvent.Api.Common;

namespace GameClubAndEvent.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private static string[] _commonError = ["Something went wrong, please retry in a few minute."];

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
            await context.Response.WriteAsJsonAsync(new Result<object>
            {
                Errors = _commonError,
                StatusCode = 500
            });

            _logger.LogError(ex, "Exception: {Message}", ex.Message);
        }
    }
}
