using System.Threading.Tasks;
using System.Threading;

namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Net.Http;

  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  using Sitecore.MobileSDK.API.Request;


  public class GetMediaContentHashTask : IRestApiCallTasks
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


    public Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken)
    {
      return null;
    }

    public Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
      return null;
    }

    public Task<string> ParseResponseDataAsync(string httpData, CancellationToken cancelToken)
    {
      return null;
    }

  }
}

