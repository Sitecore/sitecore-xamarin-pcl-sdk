namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Threading;
  using System.Threading.Tasks;
  using System.Net.Http;
  using System.Text;

  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.PublicKey;

  public class UploadMediaTask : AbstractGetItemTask<IMediaResourceUploadRequest>
  {
    public UploadMediaTask(UploadMediaUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
    }
      
    public override async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IMediaResourceUploadRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Post, url);
      string imageData = System.Text.Encoding.UTF8.GetString (request.createMediaParameters.ImageData);
      result.Content = new StringContent(imageData, Encoding.UTF8, "multipart/form-data");
      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
      return result;
    }


    protected override string UrlToGetItemWithRequest(IMediaResourceUploadRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly UploadMediaUrlBuilder urlBuilder;
  }
}
