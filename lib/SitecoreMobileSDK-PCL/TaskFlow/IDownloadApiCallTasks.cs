namespace Sitecore.MobileSDK.TaskFlow
{
  using System.Threading;
  using System.Threading.Tasks;

  internal interface IDownloadApiCallTasks<TRequest, THttpRequest, THttpResult>
    where TRequest : class
    where THttpRequest : class
    where THttpResult : class
  {
    THttpRequest BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken);

    Task<THttpResult> SendRequestForUrlAsync(THttpRequest requestUrl, CancellationToken cancelToken);
  }
}

