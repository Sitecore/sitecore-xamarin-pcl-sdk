
namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.Exceptions;


    public class RestApiCallFlow
    {
		public static async Task<TResult> LoadRequestFromNetworkFlow<TRequest, THttpRequest, THttpResult, TResult>(
            TRequest request, 
            IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult> stages,
            CancellationToken cancelToken)
        where TRequest: class
        where THttpRequest: class
        where THttpResult: class
        where TResult: class
        {
            THttpRequest requestUrl = null;
            THttpResult serverResponse = null;
            TResult parsedData = null;

            if (null == request)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] user's request cannot be null");
            }


            try
            {
                requestUrl = await stages.BuildRequestUrlForRequestAsync (request, cancelToken);
            }
            catch (ObjectDisposedException)
            {
                // CancellationToken.ThrowIfCancellationRequested()
                throw;
            }
            catch (OperationCanceledException)
            {
                // CancellationToken.ThrowIfCancellationRequested()
                // and TaskCanceledException
                throw;
            }
            catch (Exception ex)
            {
                throw new ProcessUserRequestException ("[Sitecore Mobile SDK] Unable to build HTTP request", ex);
            }
            ////


            if (null == requestUrl)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] http request cannot be null");
            }

            try
            {
                serverResponse = await stages.SendRequestForUrlAsync (requestUrl, cancelToken);
            }
            catch (ObjectDisposedException)
            {
                // CancellationToken.ThrowIfCancellationRequested()
                throw;
            }
            catch (OperationCanceledException)
            {
                // CancellationToken.ThrowIfCancellationRequested()
                // and TaskCanceledException
                throw;
            }
            catch (Exception ex)
            {
                throw new LoadDataFromNetworkException ("[Sitecore Mobile SDK] Unable to download data from the internet", ex);
            }
            ////


            if (null == serverResponse)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] back end response cannot be null");
            }


            try
            {
                parsedData = await stages.ParseResponseDataAsync (serverResponse, cancelToken);
            }
            catch (ObjectDisposedException)
            {
                // CancellationToken.ThrowIfCancellationRequested()
                throw;
            }
            catch (OperationCanceledException)
            {
                // CancellationToken.ThrowIfCancellationRequested()
                // and TaskCanceledException
                throw;
            }
            catch (Exception ex)
            {
                throw new ParserException("[Sitecore Mobile SDK] Unable to download data from the internet", ex);
            }


            if (null == parsedData)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] parsed response cannot be null");
            }

            return parsedData;
        }
    }
}

