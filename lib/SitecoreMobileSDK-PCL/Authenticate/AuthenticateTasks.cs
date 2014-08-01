
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
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  class AuthenticateTasks : IRestApiCallTasks<ISessionConfig, HttpRequestMessage, string, WebApiJsonStatusMessage>
  {
    #region Private Variables

    private readonly IRestServiceGrammar restGrammar;
    private readonly IWebApiUrlParameters webApiGrammar;
    private readonly SessionConfigUrlBuilder urlBuilder;
    private readonly HttpClient httpClient;
    private readonly ICredentialsHeadersCryptor credentialsCryptor;

    #endregion Private Variables

    public AuthenticateTasks(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar,
      SessionConfigUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor cryptor)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;
      this.credentialsCryptor = cryptor;
    }

    #region IRestApiCallTasks

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(ISessionConfig request, CancellationToken cancelToken)
    {
      string url = this.PrepareRequestUrl(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

      return await this.credentialsCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestMessage, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + requestMessage);
      HttpResponseMessage httpResponse = await this.httpClient.SendAsync(requestMessage, cancelToken);
      HttpContent responseContent = httpResponse.Content;

      return await responseContent.ReadAsStringAsync();
    }

    public async Task<WebApiJsonStatusMessage> ParseResponseDataAsync(string httpData, CancellationToken cancelToken)
    {
      Func<WebApiJsonStatusMessage> syncParsePublicKey = () =>
      {
        return AuthenticateResponseParser.ParseResponse(httpData, cancelToken);
      };

      return await Task.Factory.StartNew(syncParsePublicKey, cancelToken);
    }

    private string PrepareRequestUrl(ISessionConfig request)
    {
      return this.urlBuilder.BuildUrlString(request)
        + this.restGrammar.PathComponentSeparator
        + this.webApiGrammar.ItemWebApiActionsEndpoint
        + this.webApiGrammar.ItemWebApiAuthenticateAction;
    }

    #endregion IRestApiCallTasks
  }
}
