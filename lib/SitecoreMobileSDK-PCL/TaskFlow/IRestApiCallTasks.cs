


namespace Sitecore.MobileSDK.TaskFlow
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;


  public interface IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult> : IDownloadApiCallTasks<TRequest, THttpRequest, THttpResult>
    where TRequest: class
    where THttpRequest: class
    where THttpResult: class
    where TResult: class
  {
    Task<TResult> ParseResponseDataAsync(THttpResult httpData, CancellationToken cancelToken);
  }
}

