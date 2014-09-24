﻿using Sitecore.MobileSDK.PublicKey;


namespace Sitecore.MobileSDK.CrudTasks.Resource
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.RenderingHTML;

  public class GetRenderingHTMLTasks : IDownloadApiCallTasks<IGetRenderingHtmlRequest, HttpRequestMessage, string>
  {
    private GetRenderingHTMLTasks()
    {
    }

    public GetRenderingHTMLTasks(RenderingHTMLUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
      this.httpClient = httpClient;
      this.credentialsHeadersCryptor = credentialsHeadersCryptor;

      this.Validate();
    }

    #region  IRestApiCallTasks

    public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(IGetRenderingHtmlRequest request, CancellationToken cancelToken)
    {
      string url = this.urlBuilder.GetUrlForRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

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

    #endregion IRestApiCallTasks

    private void Validate()
    {
      if (null == this.httpClient)
      {
        throw new ArgumentNullException("AbstractGetItemTask.httpClient cannot be null");
      }
    }

    private readonly RenderingHTMLUrlBuilder urlBuilder;
    private HttpClient httpClient;
    private ICredentialsHeadersCryptor credentialsHeadersCryptor;
  }
}




