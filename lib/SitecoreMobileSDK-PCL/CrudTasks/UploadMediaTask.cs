﻿
namespace Sitecore.MobileSDK.CrudTasks
{
  using System.IO;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Diagnostics;

  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  internal class UploadMediaTask : AbstractGetItemTask<IMediaResourceUploadRequest>
  {

    public UploadMediaTask(UploadMediaUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
      this.credentialsHeadersCryptor = credentialsHeadersCryptor;
      this.httpClient = httpClient;
    }
      
    public override async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IMediaResourceUploadRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Post, url);

      byte[] data = this.ReadFully(request.UploadOptions.ImageDataStream);

      MultipartFormDataContent multiPartContent = new MultipartFormDataContent();

      ByteArrayContent byteArrayContent = new ByteArrayContent (data);

      byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse(request.UploadOptions.ContentType);

      multiPartContent.Add(byteArrayContent);

      //@igk server side issue, the following parameters must contaign quotes
      string name = "\"datafile\"";
      string filename = "\"" + request.UploadOptions.FileName + "\"";
      //@igk hack to have name/filename parameters with quotes under the "Content-Disposition" header, do not move this
      //to the multiPartContent.Add(..) method
      byteArrayContent.Headers.Add ("Content-Disposition", "form-data; name=" + name + "; filename=" + filename);
       
      result.Content = multiPartContent;

      result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);

      return result;

    }

    protected override string UrlToGetItemWithRequest(IMediaResourceUploadRequest request)
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
