using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TW.Infrastructure.ApsNetCore.Middlewares;

public class RequestResponseRecorderMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseRecorderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestResponseRecorderMiddleware> logger)
    {
        context.Request.EnableBuffering();

        var requestContent = await GetStreamContent(context.Request.Body);
        string responseContent;
        var originalResponseStream = context.Response.Body;

        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            await _next(context);

            responseContent = await GetStreamContent(memoryStream);

            await memoryStream.CopyToAsync(originalResponseStream);
            context.Response.Body = originalResponseStream;
        }

        var message = new
        {
            method = context.Request.Method,
            url = GetUrl(context.Request),
            headers = GetHeader(context.Request),
            request = requestContent,
            response = responseContent
        };

        logger.LogInformation(JsonSerializer.Serialize(message));
    }

    private string GetUrl(HttpRequest request) =>
        $"{request.Scheme}://{request.Host}:{request.Path}?{request.QueryString}";

    private string GetHeader(HttpRequest request) =>
        request.Headers != null && request.Headers.Any()
            ? string.Join(",", request.Headers.Select(d => $"{d.Key}={string.Join(",", d.Value)}"))
            : string.Empty;

    private async Task<string> GetStreamContent(Stream stream)
    {
        var content = string.Empty;
        if (stream != null)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(stream);
            content = await streamReader.ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);
        }

        return content;
    }
}