namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  using Sitecore.MobileSDK.API.Request;


  internal class GetMediaContentHashTask : IRestApiCallTasks
  <
    IMediaResourceDownloadRequest, 
    HttpRequestMessage, 
    string, // JSON response
    string  // parsed URL
  >
  {
    private HttpClient httpClient;
    MediaItemUrlBuilder urlBuilder;
    private ICredentialsHeadersCryptor credentialsHeadersCryptor;

    public GetMediaContentHashTask(
      MediaItemUrlBuilder urlBuilder, 
      HttpClient httpClient, 
      ICredentialsHeadersCryptor credentialsHeadersCryptor)
    {
      this.httpClient = httpClient;
      this.urlBuilder = urlBuilder;
      this.credentialsHeadersCryptor = credentialsHeadersCryptor;
    }


    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken)
    {
      string url = this.urlBuilder.BuildUrlToRequestHashForPath(request.MediaPath, request.DownloadOptions);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
      return result;
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
      return null;
    }

    public async Task<string> ParseResponseDataAsync(string httpData, CancellationToken cancelToken)
    {
      return null;
    }

  }
}

