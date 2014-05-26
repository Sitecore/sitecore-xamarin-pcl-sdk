

namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading.Tasks;


    public class RestApiCallFlow
    {
        public static async Task<TResult> LoadRequestFromNetworkFlow<TRequest, THttpResult, TResult>(TRequest request, IRestApiCallTasks<TRequest, THttpResult, TResult> stages)
        {
            string requestUrl = await stages.BuildRequestUrlForRequestAsync(request);
            THttpResult serverResponse = await stages.SendRequestForUrlAsync(requestUrl);
            TResult parsedData = await stages.ParseResponseDataAsync(serverResponse);

            return parsedData;
        }
    }
}

