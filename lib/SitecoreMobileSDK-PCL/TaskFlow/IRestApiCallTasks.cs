namespace Sitecore.MobileSDK.TaskFlow
{
  using System.Threading;
  using System.Threading.Tasks;

  internal interface IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult> : IDownloadApiCallTasks<TRequest, THttpRequest, THttpResult>
    where TRequest : class
    where THttpRequest : class
    where THttpResult : class
    where TResult : class
  {
    Task<TResult> ParseResponseDataAsync(THttpResult httpData, CancellationToken cancelToken);
  }
}

