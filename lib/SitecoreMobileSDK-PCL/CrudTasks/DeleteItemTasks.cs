namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Diagnostics;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;

  class DeleteItemTasks<T> : IRestApiCallTasks<T, HttpRequestMessage, string, ScDeleteItemsResponse>
    where T : class, IBaseDeleteItemRequest
  {
    private readonly IDeleteItemsUrlBuilder<T> deleteItemsBuilder;
    private readonly HttpClient httpClient;
    private readonly ICredentialsHeadersCryptor cryptor;

    public DeleteItemTasks(IDeleteItemsUrlBuilder<T> deleteItemsBuilder, HttpClient httpClient,
      ICredentialsHeadersCryptor cryptor)
    {
      this.deleteItemsBuilder = deleteItemsBuilder;
      this.httpClient = httpClient;
      this.cryptor = cryptor;

      this.Validate();
    }

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(T request, CancellationToken cancelToken)
    {

      var url = this.deleteItemsBuilder.GetUrlForRequest(request);
      var result = new HttpRequestMessage(HttpMethod.Delete, url);

      return await this.cryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
      var result = await this.httpClient.SendAsync(request, cancelToken);
      return await result.Content.ReadAsStringAsync();
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

      if (null == this.cryptor)
      {
        throw new ArgumentNullException("DeleteItemTasks.cryptor cannot be null");
      }

      if (null == this.deleteItemsBuilder)
      {
        throw new ArgumentNullException("DeleteItemTasks.deleteItemsBuilder cannot be null");
      }
    }
  }
}
