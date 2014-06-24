

namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Diagnostics;
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class GetMediaItemTask : IDownloadApiCallTasks<IReadMediaItemRequest, HttpRequestMessage, Stream>
  {
    private GetMediaItemTask()
    {
    }

    public GetMediaItemTask(MediaItemUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;
      this.credentialsHeadersCryptor = credentialsHeadersCryptor;

      this.Validate ();
    }

    #region  IRestApiCallTasks

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IReadMediaItemRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest((IReadMediaItemRequest)request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
      return result;
    }

    public async Task<Stream> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
		  //TODO: @igk debug request output, remove later
	    Debug.WriteLine("REQUEST: " + requestUrl);

      Stream httpResponse = await this.httpClient.GetStreamAsync(requestUrl.RequestUri.AbsoluteUri);
      return httpResponse;
    }

    #endregion IRestApiCallTasks

    private void Validate()
    {
      if (null == this.httpClient)
      {
        throw new ArgumentNullException ("AbstractGetItemTask.httpClient cannot be null");
      }
      else if (null == this.credentialsHeadersCryptor)
      {
        throw new ArgumentNullException ("AbstractGetItemTask.credentialsHeadersCryptor cannot be null");
      }
    }

    protected string UrlToGetItemWithRequest(IReadMediaItemRequest request)
    {
      return this.urlBuilder.BuildUrlStringForPath(request.MediaItemPath, request.DownloadOptions);
    }

    private readonly MediaItemUrlBuilder urlBuilder;
    private HttpClient httpClient;
    private ICredentialsHeadersCryptor credentialsHeadersCryptor;
  }
}




