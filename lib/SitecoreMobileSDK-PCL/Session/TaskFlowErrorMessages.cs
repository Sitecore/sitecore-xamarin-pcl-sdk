namespace Sitecore.MobileSDK.Session
{
  using System;

  internal static class TaskFlowErrorMessages
  {
    public const string NETWORK_EXCEPTION_MESSAGE = "[Sitecore Mobile SDK] Unable to download data from the internet";
    public const string BAD_USER_REQUEST_MESSAGE = "[Sitecore Mobile SDK] Unable to build HTTP request";
    public const string PARSER_EXCEPTION_MESSAGE = "[Sitecore Mobile SDK] Data from the internet has unexpected format";

    public const string PARSER_RESULT_NULL_MESSAGE = "[RestApiCallFlow.LoadRequestFromNetworkFlow] parsed response cannot be null";
    public const string USER_REQUEST_NULL_MESSAGE = "[RestApiCallFlow.LoadRequestFromNetworkFlow] user's request cannot be null";
    public const string HTTP_REQUEST_NULL_MESSAGE = "[RestApiCallFlow.LoadRequestFromNetworkFlow] http request cannot be null";
    public const string SERVER_RESPONSE_NULL_MESSAGE = "[RestApiCallFlow.LoadRequestFromNetworkFlow] back end response cannot be null";
  }
}

