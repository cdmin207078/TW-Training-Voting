using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TW.Infrastructure.ApsNetCore.Middlewares;

public class GlobalExceptionCatcherMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionCatcherMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionCatcherMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var error = new { code = StatusCodes.Status400BadRequest, message = ex?.Message };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
                
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var error = new { code = StatusCodes.Status500InternalServerError, message = "Internal Server Error" };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}