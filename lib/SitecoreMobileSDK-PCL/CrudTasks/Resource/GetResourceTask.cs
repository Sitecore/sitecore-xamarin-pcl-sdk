

namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Diagnostics;
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class GetResourceTask : IDownloadApiCallTasks<IReadMediaItemRequest, HttpRequestMessage, Stream>
  {
    private GetResourceTask()
    {
    }

    public GetResourceTask(MediaItemUrlBuilder urlBuilder, HttpClient httpClient)
    {
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;

      this.Validate ();
    }

    #region  IRestApiCallTasks

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IReadMediaItemRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest((IReadMediaItemRequest)request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

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
    }

    protected string UrlToGetItemWithRequest(IReadMediaItemRequest request)
    {
      return this.urlBuilder.BuildUrlStringForPath(request.MediaPath, request.DownloadOptions);
    }

    private readonly MediaItemUrlBuilder urlBuilder;
    private HttpClient httpClient;
  }
}




