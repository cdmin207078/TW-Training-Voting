namespace TW.Infrastructure.Core.Components.HttpClients;

public interface IHttpClientService
{
    Task<TResponse> Get<TResponse>(string url, Dictionary<string, string>? headers = null) where TResponse : class;
    Task<TResponse> Get<TResponse>(Uri uri, Dictionary<string, string>? headers = null) where TResponse : class;

    Task<TResponse> Post<TResponse>(string url, object? body, Dictionary<string, string>? headers = null)
        where TResponse : class;
    Task<TResponse> Post<TResponse>(Uri uri, object? body, Dictionary<string, string>? headers = null)
        where TResponse : class;

    Task<TResponse> Put<TResponse>(string url, object? body, Dictionary<string, string>? headers = null)
        where TResponse : class;
    Task<TResponse> Put<TResponse>(Uri uri, object? body, Dictionary<string, string>? headers = null)
        where TResponse : class;

    Task<TResponse> Delete<TResponse>(string url, object? body, Dictionary<string, string>? headers = null)
        where TResponse : class;
    Task<TResponse> Delete<TResponse>(Uri uri, object? body, Dictionary<string, string>? headers = null)
        where TResponse : class;
}