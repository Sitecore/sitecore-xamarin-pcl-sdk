using System.Text;


namespace Sitecore.MobileSDK.PublicKey
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.PasswordProvider.Interface;

  public class GetPublicKeyTasks : IRestApiCallTasks<ISessionConfig, string, Stream, string>
  {
    #region Private Variables

    private readonly SessionConfigUrlBuilder sessionConfigBuilder;
    private readonly ISSCUrlParameters sscGrammar;
    private readonly HttpClient httpClient;
    private readonly ISSCCredentials credentials;
    private string domain;

    #endregion Private Variables

    public GetPublicKeyTasks(ISSCCredentials credentials, SessionConfigUrlBuilder sessionConfigBuilder, ISSCUrlParameters sscGrammar, HttpClient httpClient)
    {
      this.sessionConfigBuilder = sessionConfigBuilder;
      this.sscGrammar = sscGrammar;
      this.httpClient = httpClient;
      this.credentials = credentials;
    }

    #region IRestApiCallTasks

    public string BuildRequestUrlForRequestAsync(ISessionConfig request, CancellationToken cancelToken)
    {
      this.domain = request.Site;
      return this.PrepareRequestUrl(request);
    }

    public async Task<Stream> SendRequestForUrlAsync(string requestUrl, CancellationToken cancelToken)
    {
      Debug.WriteLine("REQUEST: " + requestUrl);

      //TODO: @igk extract
      var stringContent = new StringContent("{\"domain\":\""
                                            + this.domain
                                            +"\",\"username\":\""
                                            + this.credentials.Username
                                            + "\",\"password\":\""
                                            + this.credentials.Password
                                            + "\"}", Encoding.UTF8, "application/json");
      
      HttpResponseMessage httpResponse = await this.httpClient.PostAsync(requestUrl, stringContent, cancelToken);

      HttpContent responseContent = httpResponse.Content;

      Stream result = await responseContent.ReadAsStreamAsync();
      return result;
    }

    // disposes httpData
    public async Task<string> ParseResponseDataAsync(Stream httpData, CancellationToken cancelToken)
    {

      using (Stream publicKeyStream = httpData)
      {
        Func<string> syncParsePublicKey = () =>
        {
          return "OK";
          //return new PublicKeyXmlParser().Parse(publicKeyStream, cancelToken);
        };
        string result = await Task.Factory.StartNew(syncParsePublicKey, cancelToken);
        return result;
      }
    }

    private string PrepareRequestUrl(ISessionConfig instanceUrl)
    {
      string url = this.sessionConfigBuilder.BuildUrlString(instanceUrl);

      //FIXME: hack to force https protocol
      if (!url.StartsWith("https", StringComparison.CurrentCulture))
      {
        url = url.Insert(4, "s");
      }

      url = url + this.sscGrammar.ItemSSCAuthEndpoint
                + this.sscGrammar.ItemSSCLoginAction;
      
      return url;
    }

    #endregion IRestApiCallTasks
  }
}

