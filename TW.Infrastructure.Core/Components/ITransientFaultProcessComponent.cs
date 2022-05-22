namespace TW.Infrastructure.Core.Components;

public interface ITransientFaultProcessComponent
{
    protected const int _defRetryCount = 3;

    #region For Exception

    TResult Execute<TException, TResult>(Func<TResult> action, int retryCount = _defRetryCount)
        where TException : Exception;

    Task<TResult> Execute<TException, TResult>(Task<TResult> action)
        where TException : Exception;

    TResult Execute<TException, TResult>(Func<TResult> action, int retryCount, string callerName)
        where TException : Exception;

    Task<TResult> Execute<TException, TResult>(Task<TResult> action, int retryCount, string callerName)
        where TException : Exception;
    
    #endregion
        
    #region For Result

    TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> retryPredicated);

    Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> retryPredicated);
    
    TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> retryPredicated, int retryCount, string callerName);

    Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> retryPredicated, int retryCount, string callerName);

    #endregion

    #region For Timeout

    #endregion

    #region For Rate-Limit

    #endregion
}