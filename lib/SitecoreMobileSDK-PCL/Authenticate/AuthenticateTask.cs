namespace Sitecore.MobileSDK.Authenticate
{
  using System;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  class AuthenticateTask : IRestApiCallTasks<ISessionConfig, HttpRequestMessage, string, WebApiJsonStatusMessage>
  {

    #region Private Variables

    private readonly IWebApiUrlParameters webApiGrammar;
    private readonly SessionConfigUrlBuilder urlBuilder;
    private readonly HttpClient httpClient;
    private readonly ICredentialsHeadersCryptor credentialsCryptor;

    #endregion Private Variables

    public AuthenticateTask(IWebApiUrlParameters webApiGrammar, SessionConfigUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor cryptor)
    {
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

    private string PrepareRequestUrl(ISessionConfig request)
    {
      return this.urlBuilder.BuildUrlString(request)
        + "/"
        + this.webApiGrammar.ItemWebApiActionsEndpoint
        + this.webApiGrammar.ItemWebApiAuthenticateAction;
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestMessage, CancellationToken cancelToken)
    {
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

    #endregion IRestApiCallTasks
  }
}
