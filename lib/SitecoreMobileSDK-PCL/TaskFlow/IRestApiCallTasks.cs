
namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading.Tasks;


    public interface IRestApiCallTasks<TRequest, THttpResult, TResult>
    {
        Task<string> BuildRequestUrlForRequestAsync(TRequest request);

        Task<THttpResult> SendRequestForUrlAsync(string requestUrl);
        
        Task<TResult> ParseResponseDataAsync(THttpResult httpData);
    }
}

