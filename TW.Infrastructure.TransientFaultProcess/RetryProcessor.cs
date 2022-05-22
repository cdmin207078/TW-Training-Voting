using System.Text.Json;
using Microsoft.Extensions.Logging;
using Polly;
using TW.Infrastructure.Core.Components.TransientFalutProcess;

namespace TW.Infrastructure.TransientFaultProcess;

public class RetryProcessor : IRetryProcessor
{
    private readonly ILogger<RetryProcessor> _logger;

    public RetryProcessor(ILogger<RetryProcessor> logger)
    {
        _logger = logger;
    }
    
    private void OnRetry(Exception exception, int retryIndex, Context context)
    {
        _logger.LogInformation($"#{retryIndex}: {JsonSerializer.Serialize(context)}, Exception: {exception.Message}");
    }
    
    private void OnRetry<TResult>(DelegateResult<TResult> result, int retryIndex, Context context)
    {
        _logger.LogInformation($"#{retryIndex}: {JsonSerializer.Serialize(context)}, Result: {result.Result}");
    }
    
    public TResult Execute<TException, TResult>(Func<TResult> action, int retryCount = 3) where TException : Exception
    {
        return Policy.Handle<TException>().Retry(retryCount, OnRetry).Execute(ctx => action.Invoke(), null);
    }

    public Task<TResult> Execute<TException, TResult>(Task<TResult> action, int retryCount = 3) where TException : Exception
    {
        return Policy.Handle<TException>().RetryAsync(retryCount, OnRetry).ExecuteAsync(ctx => action, null);
    }

    public TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> resultPredicate, int retryCount = 3)
    {
        return Policy.HandleResult(resultPredicate).Retry(retryCount, OnRetry).Execute(ctx => action.Invoke(), null);
    }

    public Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> resultPredicate, int retryCount = 3)
    {
        return Policy.HandleResult(resultPredicate).RetryAsync(retryCount, OnRetry).ExecuteAsync(ctx => action, null);
    }
}