namespace Sitecore.MobileSDK.TaskFlow
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.API.Exceptions;

  internal class RestApiCallFlow
  {
    private static async Task<TPhaseResult> IvokeTaskAndWrapExceptions<TPhaseResult, TWrapperException>(
      Task<TPhaseResult> task, 
      Func<Exception, TWrapperException> exceptionWrapperDelegate)
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
        throw exceptionWrapperDelegate(ex);
      }

      return result;
    }


    public static async Task<TResult> LoadRequestFromNetworkFlow<TRequest, THttpRequest, THttpResult, TResult>(
      TRequest request,
      IRestApiCallTasks<TRequest, THttpRequest, THttpResult, TResult> stages,
      CancellationToken cancelToken)
    where TRequest : class
    where THttpRequest : class
    where THttpResult : class
    where TResult : class
    {
      THttpResult serverResponse = null;
      TResult parsedData = null;

      serverResponse = await RestApiCallFlow.LoadResourceFromNetworkFlow(request, stages, cancelToken);

      Func<Exception, ParserException> parseExceptionWrapper = (Exception ex) => new ParserException(TaskFlowErrorMessages.PARSER_EXCEPTION_MESSAGE, ex);
      Task<TResult> asyncParser = stages.ParseResponseDataAsync(serverResponse, cancelToken);

      parsedData = await RestApiCallFlow.IvokeTaskAndWrapExceptions(asyncParser, parseExceptionWrapper);
      ////

      if (null == parsedData)
      {
        throw new ArgumentNullException(TaskFlowErrorMessages.PARSER_RESULT_NULL_MESSAGE);
      }

      return parsedData;
    }

    public static async Task<THttpResult> LoadResourceFromNetworkFlow<TRequest, THttpRequest, THttpResult>(
      TRequest request,
      IDownloadApiCallTasks<TRequest, THttpRequest, THttpResult> stages,
      CancellationToken cancelToken)
    where TRequest : class
    where THttpRequest : class
    where THttpResult : class
    {
      THttpRequest requestUrl = null;
      THttpResult serverResponse = null;

      if (null == request)
      {
        throw new ArgumentNullException(TaskFlowErrorMessages.USER_REQUEST_NULL_MESSAGE);
      }


      Func<Exception, ProcessUserRequestException> urlExceptionWrapper = (Exception ex) => new ProcessUserRequestException(TaskFlowErrorMessages.BAD_USER_REQUEST_MESSAGE, ex);
      requestUrl = stages.BuildRequestUrlForRequestAsync(request, cancelToken);

      if (null == requestUrl)
      {
        throw new ArgumentNullException(TaskFlowErrorMessages.HTTP_REQUEST_NULL_MESSAGE);
      }

      Func<Exception, LoadDataFromNetworkException> httpExceptionWrapper = (Exception ex) =>
      {
        return new LoadDataFromNetworkException(TaskFlowErrorMessages.NETWORK_EXCEPTION_MESSAGE, ex);
      };
      Task<THttpResult> httpLoader = stages.SendRequestForUrlAsync(requestUrl, cancelToken);

      serverResponse = await RestApiCallFlow.IvokeTaskAndWrapExceptions(httpLoader, httpExceptionWrapper);
      ////


      if (null == serverResponse)
      {
        throw new ArgumentNullException(TaskFlowErrorMessages.SERVER_RESPONSE_NULL_MESSAGE);
      }

      return serverResponse;
    }
  }
}

