using System.Text.Json;
using Microsoft.Extensions.Logging;
using TW.Infrastructure.Core.Components.HttpClients;
using RestSharp;

namespace TW.Infrastructure.RestsharpHttpClient;

public class RestsharpHttpClientService : IHttpClientService
{
    private readonly RestClient _client;
    private readonly ILogger<RestsharpHttpClientService> _logger;

    public RestsharpHttpClientService(ILogger<RestsharpHttpClientService> logger)
    {
        _logger = logger;
        _client = new RestClient();
    }

    private async Task<TResponse> Execute<TResponse>(
        Uri uri,
        Method method,
        Dictionary<string, string>? headers = null,
        object? body = null)
        where TResponse : class
    {
        var input = new { uri, method, headers, body };
        
        try
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            
            var request = new RestRequest(uri);
            if (headers is not null && headers.Any())
                request.AddHeaders(headers);
            
            if (body is not null)
                request.AddJsonBody(body);
            
            Task<TResponse> response;
            
            if (method == Method.Get)
                response = _client.GetAsync<TResponse>(request);
            else if (method == Method.Post)
                response = _client.PostAsync<TResponse>(request);
            else if (method == Method.Put)
                response = _client.PutAsync<TResponse>(request);
            else if (method == Method.Delete)
                response = _client.DeleteAsync<TResponse>(request);
            else 
                throw new ArgumentException($"Not currently supported method type: {method}");
            
            var output = await response;
            
            _logger.LogInformation($"{JsonSerializer.Serialize(new { request = input, response = output })}");

            return output;
        }
        catch (Exception e)
        {
            _logger.LogError($"{JsonSerializer.Serialize(new { request = input, response = e?.Message ?? e.InnerException?.Message })}");
            throw;
        }
    }

    public Task<TResponse> Get<TResponse>(string url, Dictionary<string, string>? headers = null)
        where TResponse : class
        => Execute<TResponse>(new Uri(url), Method.Get, headers);

    public Task<TResponse> Get<TResponse>(Uri uri, Dictionary<string, string>? headers = null) where TResponse : class
        => Execute<TResponse>(uri, Method.Get, headers);

    public Task<TResponse> Post<TResponse>(string url, object? body, Dictionary<string, string>? headers = null) where TResponse : class       
        => Execute<TResponse>(new Uri(url), Method.Post, headers, body);

    public Task<TResponse> Post<TResponse>(Uri uri, object? body, Dictionary<string, string>? headers = null) where TResponse : class
        => Execute<TResponse>(uri, Method.Post, headers, body);
    
    public Task<TResponse> Put<TResponse>(string url, object? body, Dictionary<string, string>? headers = null) where TResponse : class
        => Execute<TResponse>(new Uri(url), Method.Put, headers, body);

    public Task<TResponse> Put<TResponse>(Uri uri, object? body, Dictionary<string, string>? headers = null) where TResponse : class
        => Execute<TResponse>(uri, Method.Put, headers, body);
    
    public Task<TResponse> Delete<TResponse>(string url, object? body, Dictionary<string, string>? headers = null) where TResponse : class
        => Execute<TResponse>(new Uri(url), Method.Delete, headers, body);

    public Task<TResponse> Delete<TResponse>(Uri uri, object? body, Dictionary<string, string>? headers = null) where TResponse : class
        => Execute<TResponse>(uri, Method.Delete, headers, body);
}