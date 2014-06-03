

namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading.Tasks;


    public class RestApiCallFlow
    {
		public static async Task<TResult> LoadRequestFromNetworkFlow<TRequest, THttpRequest, THttpResult, TResult>(TRequest request, IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult> stages)
        {
            if (null == request)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] user's request cannot be null");
            }


			THttpRequest requestUrl = await stages.BuildRequestUrlForRequestAsync(request);
            if (null == requestUrl)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] http request cannot be null");
            }


            THttpResult serverResponse = await stages.SendRequestForUrlAsync(requestUrl);
            if (null == serverResponse)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] back end response cannot be null");
            }


            TResult parsedData = await stages.ParseResponseDataAsync(serverResponse);
            if (null == parsedData)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] parsed response cannot be null");
            }

            return parsedData;
        }
    }
}

