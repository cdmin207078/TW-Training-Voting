namespace TW.Infrastructure.Core.Components.TransientFalutProcess;

public interface IRetryProcessor
{
    TResult Execute<TException, TResult>(Func<TResult> action, TimeSpan[] retryTimes) where TException : Exception;
    
    Task<TResult> Execute<TException, TResult>(Task<TResult> action, TimeSpan[] retryTimes) where TException : Exception;

    TResult Execute<TResult>(Func<TResult> action, Func<TResult, bool> resultPredicate, TimeSpan[] retryTimes);

    Task<TResult> Execute<TResult>(Task<TResult> action, Func<TResult, bool> resultPredicate, TimeSpan[] retryTimes);

    TResult Execute<TException, TResult>(Func<TResult> action, Func<TResult, bool> resultPredicate,
        TimeSpan[] retryTimes)
        where TException : Exception;

    Task<TResult> Execute<TException, TResult>(Task<TResult> action, Func<TResult, bool> resultPredicate,
        TimeSpan[] retryTimes)
        where TException : Exception;
}