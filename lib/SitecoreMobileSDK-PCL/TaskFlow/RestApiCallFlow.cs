
namespace Sitecore.MobileSDK.TaskFlow
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.Exceptions;


    public class RestApiCallFlow
    {



        private static async Task<TPhaseResult> IvokeTaskAndWrapExceptions<TPhaseResult, TWrapperException>(
            Task<TPhaseResult> task, Func<Exception, TWrapperException> exceptionWrapperDelegate)
            where TPhaseResult : class
            where TWrapperException : SitecoreMobileSdkException
        {
            TPhaseResult result = null;

            try 
            {
                result = await task;
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
                throw exceptionWrapperDelegate (ex);
            }

            return result;
        }


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


            Func<Exception, ProcessUserRequestException> urlExceptionWrapper = (Exception ex) => new ProcessUserRequestException ("[Sitecore Mobile SDK] Unable to build HTTP request", ex);
            Task<THttpRequest> requsetLoader = stages.BuildRequestUrlForRequestAsync (request, cancelToken);

            requestUrl = await RestApiCallFlow.IvokeTaskAndWrapExceptions( requsetLoader, urlExceptionWrapper );
            ////


            if (null == requestUrl)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] http request cannot be null");
            }

            Func<Exception, LoadDataFromNetworkException> httpExceptionWrapper = (Exception ex) => new LoadDataFromNetworkException ("[Sitecore Mobile SDK] Unable to download data from the internet", ex);
            Task<THttpResult> httpLoader = stages.SendRequestForUrlAsync (requestUrl, cancelToken);

            serverResponse = await RestApiCallFlow.IvokeTaskAndWrapExceptions( httpLoader, httpExceptionWrapper );
            ////


            if (null == serverResponse)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] back end response cannot be null");
            }

            Func<Exception, ParserException> parseExceptionWrapper = (Exception ex) => new ParserException("[Sitecore Mobile SDK] Unable to download data from the internet", ex);
            Task<TResult> asyncParser = stages.ParseResponseDataAsync (serverResponse, cancelToken);

            parsedData = await RestApiCallFlow.IvokeTaskAndWrapExceptions( asyncParser, parseExceptionWrapper );
            ////

            if (null == parsedData)
            {
                throw new ArgumentNullException ("[RestApiCallFlow.LoadRequestFromNetworkFlow] parsed response cannot be null");
            }

            return parsedData;
        }
    }
}

