using System.Text.Json;
using Petalaka.Account.Core.ExceptionCustom;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.API.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CoreException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = ex.StatusCode;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new LowerCaseJsonNamingPolicy(),
                WriteIndented = true // Optional: For pretty printing
            };
            var result = JsonSerializer.Serialize(new { ex.StatusCode, ex.ErrorMessage}, options);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new LowerCaseJsonNamingPolicy(),
                WriteIndented = true // Optional: For pretty printing
            };
            var result = JsonSerializer.Serialize(new { error = $"An unexpected error occurred. Detail{ex.Message}" }, options);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
        
    }
}