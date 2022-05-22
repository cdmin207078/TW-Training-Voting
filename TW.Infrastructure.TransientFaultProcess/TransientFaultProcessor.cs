// using System.Security.Principal;
// using System.Text.Json;
// using Microsoft.Extensions.Logging;
// using Polly;
// using TW.Infrastructure.Core.Components;
//
// namespace TW.Infrastructure.TransientFaultProcess;
//
// public class TransientFaultProcessor : ITransientFaultProcessComponent
// {
//     private readonly ILogger<TransientFaultProcessor> _logger;
//
//     public TransientFaultProcessor(ILogger<TransientFaultProcessor> logger)
//     {
//         _logger = logger;
//     }
//
//     private IDictionary<string, object> BuildProcessContext(string callerName)
//     {
//         return new Dictionary<string, object>()
//         {
//             { "caller",callerName },
//             { "correlationId", Guid.NewGuid().ToString("N") }
//         };
//     }
//
//     private void OnRetry(Exception exception, int retryIndex, Context context)
//     {
//         _logger.LogInformation($"#{retryIndex}: {JsonSerializer.Serialize(context)}, Exception: {exception.Message}");
//     }
//     
//     private void OnRetry<TResult>(DelegateResult<TResult> result, int retryIndex, Context context)
//     {
//         _logger.LogInformation($"#{retryIndex}: {JsonSerializer.Serialize(context)}, Result: {result.Result}");
//     }
//
//     public TResult RetryForException<TException, TResult>(
//         Func<TResult> action,
//         int retryCount,
//         string callerName)
//         where TException : Exception
//     {
//         return Policy.Handle<TException>()
//             .Retry(retryCount, OnRetry)
//             .Execute(ctx => action.Invoke(), BuildProcessContext(callerName));
//     }
//
//     public Task<TResult> RetryForException<TException, TResult>(
//         Task<TResult> action,
//         int retryCount, 
//         string callerName)
//         where TException : Exception
//     {
//         return Policy.Handle<TException>()
//             .RetryAsync(retryCount, OnRetry)
//             .ExecuteAsync(ctx => action, BuildProcessContext(callerName));
//     }
//
//     public TResult RetryForResult<TResult>(
//         Func<TResult> action, 
//         Func<TResult, bool> retryPredicated,
//         int retryCount, 
//         string callerName) 
//     {
//         return Policy.HandleResult<TResult>(retryPredicated)
//             .Retry(retryCount, OnRetry)
//             .Execute(ctx => action.Invoke(), BuildProcessContext(callerName));
//     }
//
//     public Task<TResult> RetryForResult<TResult>(
//         Task<TResult> action,
//         Func<TResult, bool> retryPredicated, 
//         int retryCount, 
//         string callerName)
//     {
//          return Policy.HandleResult(retryPredicated)
//              .RetryAsync(retryCount, OnRetry)
//              .ExecuteAsync(ctx => action, BuildProcessContext(callerName));
//     }
// }