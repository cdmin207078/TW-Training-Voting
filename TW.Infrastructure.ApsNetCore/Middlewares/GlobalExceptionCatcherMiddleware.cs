using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TW.Infrastructure.ApsNetCore.Contracts;
using TW.Infrastructure.Core.Exceptions;

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
        catch (TWException tex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            var error = new { code = GenernalApiResponseCode.Failure, message = tex?.Message };
            
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