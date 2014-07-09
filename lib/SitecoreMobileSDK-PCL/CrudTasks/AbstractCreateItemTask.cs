


namespace Sitecore.MobileSDK
{
  using System;
  using System.Threading.Tasks;
  using System.Net.Http;
  using System.Threading;
  using System.Text;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.CrudTasks;

  public abstract class AbstractCreateItemTask<TRequest> : AbstractGetItemTask<TRequest>
    where TRequest: class
  {
    public AbstractCreateItemTask(HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {

    }

    public override async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Post, url);
      result.Content = new StringContent("",  Encoding.UTF8, "application/x-www-form-urlencoded");
      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
      return result;
    }
  }
}

