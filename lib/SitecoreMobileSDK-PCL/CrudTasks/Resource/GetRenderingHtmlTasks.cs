namespace Sitecore.MobileSDK.CrudTasks.Resource
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.RenderingHtml;

  internal class GetRenderingHtmlTasks : IDownloadApiCallTasks<IGetRenderingHtmlRequest, HttpRequestMessage, Stream>
  {
    private GetRenderingHtmlTasks()
    {
    }

    public GetRenderingHtmlTasks(RenderingHtmlUrlBuilder urlBuilder, HttpClient httpClient)
    {
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;

      this.Validate();
    }

    #region  IRestApiCallTasks

    public HttpRequestMessage BuildRequestUrlForRequestAsync(IGetRenderingHtmlRequest request, CancellationToken cancelToken)
    {
      string url = this.urlBuilder.GetUrlForRequest(request);
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
        throw new ArgumentNullException("GetRenderingHtmlTasks.httpClient cannot be null");
      }
    }

    private readonly RenderingHtmlUrlBuilder urlBuilder;
    private HttpClient httpClient;
  }
}




