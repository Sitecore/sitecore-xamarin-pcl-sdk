using System.Net;

namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Diagnostics;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;

  internal class DeleteItemTasks<T> : IRestApiCallTasks<T, HttpRequestMessage, string, ScDeleteItemsResponse>
    where T : class, IBaseDeleteItemRequest
  {
    private readonly IDeleteItemsUrlBuilder<T> deleteItemsBuilder;
    private readonly HttpClient httpClient;

    public DeleteItemTasks(
      IDeleteItemsUrlBuilder<T> deleteItemsBuilder,
      HttpClient httpClient)
    {
      this.deleteItemsBuilder = deleteItemsBuilder;
      this.httpClient = httpClient;

      this.Validate();
    }

    public HttpRequestMessage BuildRequestUrlForRequestAsync(T request, CancellationToken cancelToken)
    {
      var url = this.deleteItemsBuilder.GetUrlForRequest(request);
      return new HttpRequestMessage(HttpMethod.Delete, url);
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + request);
      var result = await this.httpClient.SendAsync(request, cancelToken);
     // return await result.Content.ReadAsStringAsync();
      int code = (int)result.StatusCode;
      return code.ToString();
    }

    public async Task<ScDeleteItemsResponse> ParseResponseDataAsync(string httpData, CancellationToken cancelToken)
    {
      Func<ScDeleteItemsResponse> syncParseResponse = () =>
      {
        //TODO: @igk debug response output, remove later
        Debug.WriteLine("RESPONSE: " + httpData);
        return DeleteItemsResponseParser.ParseResponse(httpData, cancelToken);
      };
      return await Task.Factory.StartNew(syncParseResponse, cancelToken);
    }

    private void Validate()
    {
      if (null == this.httpClient)
      {
        throw new ArgumentNullException("DeleteItemTasks.httpClient cannot be null");
      }

      if (null == this.deleteItemsBuilder)
      {
        throw new ArgumentNullException("DeleteItemTasks.deleteItemsBuilder cannot be null");
      }
    }
  }
}
