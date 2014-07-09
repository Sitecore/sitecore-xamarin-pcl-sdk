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

  class AuthenticateTask : IRestApiCallTasks<ISessionConfig, HttpRequestMessage, string, WebApiJsonStatusMessage>
  {

    #region Private Variables

    private readonly SessionConfigUrlBuilder urlBuilder;
    private readonly HttpClient httpClient;
    private readonly ICredentialsHeadersCryptor credentialsCryptor;

    #endregion Private Variables

    public AuthenticateTask(SessionConfigUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor cryptor)
    {
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;
      this.credentialsCryptor = cryptor;
    }

    #region IRestApiCallTasks

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(ISessionConfig request, CancellationToken cancelToken)
    {

      string url = this.urlBuilder.BuildUrlString(request) + "/-/item/v1/-/actions/authenticate";
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

      return await this.credentialsCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
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
