namespace Sitecore.MobileSDK.CrudTasks.Resource
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class GetResourceTask : IDownloadApiCallTasks<IMediaResourceDownloadRequest, HttpRequestMessage, Stream>
  {
    private GetResourceTask()
    {
    }

    public GetResourceTask(MediaItemUrlBuilder urlBuilder, HttpClient httpClient)
    {
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;

      this.Validate();
    }

    #region  IRestApiCallTasks

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

      return result;
    }

    public async Task<Stream> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + requestUrl);

      var httpResponse = await this.httpClient.SendAsync(requestUrl, cancelToken);

      if (!httpResponse.IsSuccessStatusCode)
      {
        throw new HttpRequestException(httpResponse.ReasonPhrase);
      }

      return await httpResponse.Content.ReadAsStreamAsync();
    }

    #endregion IRestApiCallTasks

    private void Validate()
    {
      if (null == this.httpClient)
      {
        throw new ArgumentNullException("AbstractGetItemTask.httpClient cannot be null");
      }
    }

    protected string UrlToGetItemWithRequest(IMediaResourceDownloadRequest request)
    {
      return this.urlBuilder.BuildUrlStringForPath(request.MediaPath, request.DownloadOptions);
    }

    private readonly MediaItemUrlBuilder urlBuilder;
    private HttpClient httpClient;
  }
}




