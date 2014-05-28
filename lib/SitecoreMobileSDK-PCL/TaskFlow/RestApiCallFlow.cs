

namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading.Tasks;


    public class RestApiCallFlow
    {
		public static async Task<TResult> LoadRequestFromNetworkFlow<TRequest, THttpRequest, THttpResult, TResult>(TRequest request, IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult> stages)
        {
			THttpRequest requestUrl = await stages.BuildRequestUrlForRequestAsync(request);
            THttpResult serverResponse = await stages.SendRequestForUrlAsync(requestUrl);
            TResult parsedData = await stages.ParseResponseDataAsync(serverResponse);

            return parsedData;
        }
    }
}

