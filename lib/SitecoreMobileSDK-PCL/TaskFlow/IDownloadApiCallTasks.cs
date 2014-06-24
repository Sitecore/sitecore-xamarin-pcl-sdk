


namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;


  public interface IDownloadApiCallTasks<TRequest, THttpRequest, THttpResult>
        where TRequest: class
        where THttpRequest: class
        where THttpResult: class
    {
        Task<THttpRequest> BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken);

        Task<THttpResult> SendRequestForUrlAsync(THttpRequest requestUrl, CancellationToken cancelToken);
    }
}

