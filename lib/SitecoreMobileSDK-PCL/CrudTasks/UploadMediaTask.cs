namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Threading;
  using System.Threading.Tasks;
  using System.Net.Http;
  using System.Text;
  using Sitecore.MobileSDK.TaskFlow;
  using System.IO;
  using System.Diagnostics;
  using System.Net.Http.Headers;

  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.PublicKey;

  public class UploadMediaTask : IDownloadApiCallTasks<IMediaResourceUploadRequest, HttpRequestMessage, string>
  {

    public UploadMediaTask(UploadMediaUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
      this.credentialsHeadersCryptor = credentialsHeadersCryptor;
      this.httpClient = httpClient;
    }
      
    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IMediaResourceUploadRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
     // string url = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v1/sitecore/shell%2Fsitecore%2Fmedia%20library%2Fwhitelabel%2Fbigimagetestdata?name=TestMediaItem&template=System%2FMedia%2FUnversioned%2FImage&sc_database=web&sc_lang=en&payload=min";
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Post, url);

      byte[] data = this.ReadFully(request.CreateMediaParameters.ImageDataStream);

      MultipartFormDataContent multiPartContent = new MultipartFormDataContent();

      ByteArrayContent byteArrayContent = new ByteArrayContent (data);
      byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

      string name = "\"datafile\"";
      string filename = "\"image.jpg\"";

      multiPartContent.Add(byteArrayContent);

      byteArrayContent.Headers.Add ("Content-Disposition", "form-data; name=" + name + "; filename=" + filename);
       
      result.Content = multiPartContent;

      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);

      return result;

    }

    public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
    {
      //TODO: @igk debug request output, remove later
      Debug.WriteLine("REQUEST: " + requestUrl);

      HttpResponseMessage httpResponse = await this.httpClient.SendAsync(requestUrl, cancelToken);
      return await httpResponse.Content.ReadAsStringAsync();
    }

    protected string UrlToGetItemWithRequest(IMediaResourceUploadRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private byte[] ReadFully(Stream input)
    {
      using (MemoryStream ms = new MemoryStream())
      {
        input.CopyTo(ms);
        return ms.ToArray();
      }
    }

    private HttpClient httpClient;
    private readonly UploadMediaUrlBuilder urlBuilder;
    protected ICredentialsHeadersCryptor credentialsHeadersCryptor;
  }
}
