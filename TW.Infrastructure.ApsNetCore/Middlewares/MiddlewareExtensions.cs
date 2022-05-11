using Microsoft.AspNetCore.Builder;

namespace TW.Infrastructure.ApsNetCore.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionCatcher(this IApplicationBuilder builder)
        => builder.UseMiddleware<GlobalExceptionCatcherMiddleware>();

    public static IApplicationBuilder UseRequestResponseRecorder(this IApplicationBuilder builder)
        => builder.UseMiddleware<RequestResponseRecorderMiddleware>();
}