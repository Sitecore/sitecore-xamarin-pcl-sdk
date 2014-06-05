


namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;


	public interface IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult>
        where TRequest: class
        where THttpRequest: class
        where THttpResult: class
        where TResult: class
    {
        Task<THttpRequest> BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken);

        Task<THttpResult> SendRequestForUrlAsync(THttpRequest requestUrl, CancellationToken cancelToken);
        
        Task<TResult> ParseResponseDataAsync(THttpResult httpData, CancellationToken cancelToken);
    }
}

