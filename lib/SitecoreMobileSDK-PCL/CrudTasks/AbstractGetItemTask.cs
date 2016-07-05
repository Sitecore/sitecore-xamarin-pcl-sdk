namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Net.Http;
  using System.Diagnostics;
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.PublicKey;

  internal abstract class AbstractGetItemTask<TRequest> : IRestApiCallTasks<TRequest, HttpRequestMessage, string, ScItemsResponse>
      where TRequest : class
  {
    private AbstractGetItemTask()
    {
    }

    public AbstractGetItemTask(HttpClient httpClient)
    {
      this.httpClient = httpClient;

      this.Validate();
    }

    #region  IRestApiCallTasks

    public virtual HttpRequestMessage BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

      return result;
    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + requestUrl);
      HttpResponseMessage httpResponse = await this.httpClient.SendAsync(requestUrl, cancelToken);
      return await httpResponse.Content.ReadAsStringAsync();
    }

    public virtual async Task<ScItemsResponse> ParseResponseDataAsync(string data, CancellationToken cancelToken)
    {
      Func<ScItemsResponse> syncParseResponse = () =>
      {
        //TODO: @igk debug response output, remove later
        Debug.WriteLine("RESPONSE: " + data);
        return ScItemsParser.Parse(data, cancelToken);
      };
      return await Task.Factory.StartNew(syncParseResponse, cancelToken);
    }

    #endregion IRestApiCallTasks

    private void Validate()
    {
      if (null == this.httpClient)
      {
        throw new ArgumentNullException("AbstractGetItemTask.httpClient cannot be null");
      }

    }

    protected abstract string UrlToGetItemWithRequest(TRequest request);

    private HttpClient httpClient;
  }
}




