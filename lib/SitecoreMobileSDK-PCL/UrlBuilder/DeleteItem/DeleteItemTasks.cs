namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.TaskFlow;

  class DeleteItemTasks<T> : IRestApiCallTasks<T, HttpRequestMessage, string, ScDeleteItemsResponse> where T: class, IBaseDeleteItemRequest
  {
    private readonly IDeleteItemsUrlBuilder<T> deleteItemsBuilder;

    public DeleteItemTasks(IDeleteItemsUrlBuilder<T> deleteItemsBuilder, HttpClient httpClient, ICredentialsHeadersCryptor cryptor)
    {
      this.deleteItemsBuilder = deleteItemsBuilder;
    }

    public Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(T request, CancellationToken cancelToken)
    {
      deleteItemsBuilder.GetUrlForRequest(request);
      return null;
    }

    public Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
      return null;
    }

    public Task<ScDeleteItemsResponse> ParseResponseDataAsync(string httpData, CancellationToken cancelToken)
    {
      return null;
    }
  }
}
