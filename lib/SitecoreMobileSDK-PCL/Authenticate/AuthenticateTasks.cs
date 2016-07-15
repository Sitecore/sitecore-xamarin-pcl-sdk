namespace Sitecore.MobileSDK.Authenticate
{
  using System;
  using System.Diagnostics;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.UrlBuilder;

  class AuthenticateTasks : IRestApiCallTasks<ISessionConfig, HttpRequestMessage, string,SSCJsonStatusMessage>
  {
    #region Private Variables

    private readonly IRestServiceGrammar restGrammar;
    private readonly ISSCUrlParameters sscGrammar;
    private readonly SessionConfigUrlBuilder urlBuilder;
    private readonly HttpClient httpClient;

    #endregion Private Variables

    public AuthenticateTasks(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar,
      SessionConfigUrlBuilder urlBuilder, HttpClient httpClient)
    {
      this.restGrammar = restGrammar;
      this.sscGrammar = sscGrammar;
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;
    }

    #region IRestApiCallTasks

    public HttpRequestMessage BuildRequestUrlForRequestAsync(ISessionConfig request, CancellationToken cancelToken)
    {
      string url = this.PrepareRequestUrl(request);
      return new HttpRequestMessage(HttpMethod.Get, url);
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestMessage, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + requestMessage);
      HttpResponseMessage httpResponse = await this.httpClient.SendAsync(requestMessage, cancelToken);
      HttpContent responseContent = httpResponse.Content;

      return await responseContent.ReadAsStringAsync();
    }

    public async Task<SSCJsonStatusMessage> ParseResponseDataAsync(string httpData, CancellationToken cancelToken)
    {
      Func<SSCJsonStatusMessage> syncParsePublicKey = () =>
      {
        return AuthenticateResponseParser.ParseResponse(httpData, cancelToken);
      };

      return await Task.Factory.StartNew(syncParsePublicKey, cancelToken);
    }

    private string PrepareRequestUrl(ISessionConfig request)
    {
     SSCActionBuilder builder = new SSCActionBuilder(this.restGrammar, this.sscGrammar); 
      return builder.GetAuthenticateActionUrlForSession(request);
    }

    #endregion IRestApiCallTasks
  }
}
