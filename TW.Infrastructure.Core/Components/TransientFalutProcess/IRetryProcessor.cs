namespace TW.Infrastructure.Core.Components.TransientFalutProcess;

public interface IRetryProcessor
{
    private const int _defRetryCount = 3;
    
    TResult Execute<TException, TResult>(Func<TResult> action, int retryCount = _defRetryCount)
        where TException : Exception;

    Task<TResult> Execute<TException, TResult>(Task<TResult> action, int retryCount = _defRetryCount)
        where TException : Exception;
    
    // TResult Execute<TException, TResult>(Func<TResult> action, int retryCount, string callerName)
    //     where TException : Exception;
    //
    // Task<TResult> Execute<TException, TResult>(Task<TResult> action, int retryCount, string callerName)
    //     where TException : Exception;
    
    TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> resultPredicate,
        int retryCount = _defRetryCount);

    /// <summary>
    /// Execute Retry For Result
    /// </summary>
    /// <param name="action">will executed func</param>
    /// <param name="resultPredicate">use to determined whether retry</param>
    /// <param name="retryCount">determined retry times if match result-predicate</param>
    /// <typeparam name="TResult">ths func result type</typeparam>
    /// <returns></returns>
    Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> resultPredicate,
        int retryCount = _defRetryCount);

    // TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> retryPredicated, int retryCount, string callerName);
    //
    // Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> retryPredicated, int retryCount, string callerName);
}