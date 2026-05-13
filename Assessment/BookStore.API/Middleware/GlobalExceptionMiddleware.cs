using System.Net;

namespace BookStore.API.Middleware;

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = new ErrorResponse();

        switch (exception)
        {
            case UnauthorizedAccessException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Status = StatusCodes.Status401Unauthorized;
                response.Message = "Unauthorized access";
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.Status = StatusCodes.Status404NotFound;
                response.Message = exception.Message ?? "Resource not found";
                break;

            case ArgumentException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Status = StatusCodes.Status400BadRequest;
                response.Message = exception.Message ?? "Invalid argument";
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Status = StatusCodes.Status500InternalServerError;
                response.Message = "An internal server error occurred";
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}

public class ErrorResponse
{
    public int Status { get; set; }
    public string Message { get; set; } = null!;
}
