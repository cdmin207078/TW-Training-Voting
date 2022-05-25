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
    
    private void OnRetry(Exception exception, TimeSpan retryIndex, Context context)
    {
        _logger.LogInformation($"#{retryIndex}: {JsonSerializer.Serialize(context)}, Exception: {exception.Message}");
    }
    
    private void OnRetry<TResult>(DelegateResult<TResult> result, TimeSpan retryIndex, Context context)
    {
        _logger.LogInformation($"#{retryIndex}: {JsonSerializer.Serialize(context)}, Result: {result.Result}");
    }
    
    public TResult Execute<TException, TResult>(Func<TResult> action, TimeSpan[] retryTimes) where TException : Exception
    {
        return Policy.Handle<TException>().WaitAndRetry(retryTimes, OnRetry).Execute(ctx => action.Invoke(), null);
    }
    
    public Task<TResult> Execute<TException, TResult>(Task<TResult> action, TimeSpan[] retryTimes) where TException : Exception
    {
        return Policy.Handle<TException>().WaitAndRetryAsync(retryTimes, OnRetry).ExecuteAsync(() => action);
    }

    public TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> resultPredicate, TimeSpan[] retryTimes)
    {
        return Policy.HandleResult(resultPredicate).WaitAndRetry(retryTimes, OnRetry).Execute(ctx => action.Invoke(), null);
    }

    public Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> resultPredicate, TimeSpan[] retryTimes)
    {
        return Policy.HandleResult(resultPredicate).WaitAndRetryAsync(retryTimes, OnRetry).ExecuteAsync(() => action);
    }

    public TResult Execute<TException, TResult>(Func<TResult> action, Func<TResult, bool> resultPredicate, TimeSpan[] retryTimes)
        where TException : Exception
    {
        var forResultMatchPolicy = Policy.HandleResult(resultPredicate).WaitAndRetry(retryTimes, OnRetry);
        var forExceptionPolicy = Policy.Handle<TException>().WaitAndRetry(retryTimes, OnRetry);

        return forResultMatchPolicy.Wrap(forExceptionPolicy).Execute(() => action.Invoke());
    }

    public Task<TResult> Execute<TException, TResult>(Task<TResult> action, Func<TResult, bool> resultPredicate, TimeSpan[] retryTimes)
        where TException : Exception
    {
        var forResultMatchPolicy = Policy.HandleResult(resultPredicate).WaitAndRetryAsync(retryTimes, OnRetry);
        var forExceptionPolicy = Policy.Handle<TException>().WaitAndRetryAsync(retryTimes, OnRetry);

        return forResultMatchPolicy.WrapAsync(forExceptionPolicy).ExecuteAsync(() => action);
    }
}