namespace Sitecore.MobileSDK.PublicKey
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public class GetPublicKeyTasks : IRestApiCallTasks<string, string, Stream, PublicKeyX509Certificate>
  {
    #region Private Variables

    private readonly IWebApiUrlParameters webApiGrammar;
    private readonly HttpClient httpClient;

    #endregion Private Variables

    public GetPublicKeyTasks(IWebApiUrlParameters webApiGrammar, HttpClient httpClient)
    {
      this.webApiGrammar = webApiGrammar;
      this.httpClient = httpClient;
    }

    #region IRestApiCallTasks

    public async Task<string> BuildRequestUrlForRequestAsync(string instanceUrl, CancellationToken cancelToken)
    {
      return await Task.Factory.StartNew(() => this.PrepareRequestUrl(instanceUrl), cancelToken);
    }

    public async Task<Stream> SendRequestForUrlAsync(string requestUrl, CancellationToken cancelToken)
    {
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

    private string PrepareRequestUrl(string instanceUrl)
    {
      return SessionConfigValidator.AutocompleteInstanceUrl(instanceUrl)
        + "/"
        + this.webApiGrammar.ItemWebApiActionsEndpoint
        + this.webApiGrammar.ItemWebApiAuthenticateAction;
    }

    #endregion IRestApiCallTasks
  }
}

