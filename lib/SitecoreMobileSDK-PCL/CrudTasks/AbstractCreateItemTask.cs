using Newtonsoft.Json.Linq;
using Sitecore.MobileSDK.UrlBuilder.ChangeItem;

namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Threading.Tasks;
  using System.Net.Http;
  using System.Threading;
  using System.Text;
  using Sitecore.MobileSDK.PublicKey;

  internal abstract class AbstractCreateItemTask<TRequest> : AbstractGetItemTask<TRequest>
    where TRequest : class
  {
    public AbstractCreateItemTask(HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {

    }

    public override async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Post, url);

      string fieldsList = this.GetFieldsListString(request);
      StringContent bodycontent = new StringContent(fieldsList, Encoding.UTF8, "application/json");
      result.Content = bodycontent;

      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
      return result;
    }
      
    public abstract string GetFieldsListString(TRequest request);
  }
}

