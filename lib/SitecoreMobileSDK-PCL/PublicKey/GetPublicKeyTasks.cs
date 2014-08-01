
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
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public class GetPublicKeyTasks : IRestApiCallTasks<ISessionConfig, string, Stream, PublicKeyX509Certificate>
  {
    #region Private Variables

    private readonly SessionConfigUrlBuilder sessionConfigBuilder;
    private readonly IRestServiceGrammar restGrammar;
    private readonly IWebApiUrlParameters webApiGrammar;
    private readonly HttpClient httpClient;

    #endregion Private Variables

    public GetPublicKeyTasks(SessionConfigUrlBuilder sessionConfigBuilder, IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar, HttpClient httpClient)
    {
      this.sessionConfigBuilder = sessionConfigBuilder;
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
      this.httpClient = httpClient;
    }

    #region IRestApiCallTasks

    public async Task<string> BuildRequestUrlForRequestAsync(ISessionConfig request, CancellationToken cancelToken)
    {
      return await Task.Factory.StartNew(() => this.PrepareRequestUrl(request), cancelToken);
    }

    public async Task<Stream> SendRequestForUrlAsync(string requestUrl, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + requestUrl);
      HttpResponseMessage httpResponse = await this.httpClient.GetAsync (requestUrl, cancelToken);
      HttpContent responseContent = httpResponse.Content;

      Stream result = await responseContent.ReadAsStreamAsync ();
      return result;
    }

    // disposes httpData
    public async Task<PublicKeyX509Certificate> ParseResponseDataAsync(Stream httpData, CancellationToken cancelToken)
    {
      using (Stream publicKeyStream = httpData)
      {
        Func<PublicKeyX509Certificate> syncParsePublicKey = () =>
        {
          return new PublicKeyXmlParser().Parse(publicKeyStream, cancelToken);
        };
        PublicKeyX509Certificate result = await Task.Factory.StartNew(syncParsePublicKey, cancelToken);
        return result;
      }
    }

    private string PrepareRequestUrl(ISessionConfig instanceUrl)
    {
      return this.sessionConfigBuilder.BuildUrlString(instanceUrl)
        + this.restGrammar.PathComponentSeparator
        + this.webApiGrammar.ItemWebApiActionsEndpoint
        + this.webApiGrammar.ItemWebApiGetPublicKeyAction;
    }

    #endregion IRestApiCallTasks
  }
}

