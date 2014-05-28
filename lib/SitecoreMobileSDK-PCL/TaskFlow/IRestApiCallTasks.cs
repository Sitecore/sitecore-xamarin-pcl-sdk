
namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading.Tasks;


	public interface IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult>
    {
		Task<THttpRequest> BuildRequestUrlForRequestAsync(TRequest request);

		Task<THttpResult> SendRequestForUrlAsync(THttpRequest requestUrl);
        
        Task<TResult> ParseResponseDataAsync(THttpResult httpData);
    }
}

