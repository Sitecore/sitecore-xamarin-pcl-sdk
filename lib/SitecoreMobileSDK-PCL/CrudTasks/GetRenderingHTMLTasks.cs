namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;


  public class GetRenderingHTMLTasks : AbstractGetItemTask<IGetRenderingHtmlRequest>
  {
    public GetRenderingHTMLTasks(RenderingHTMLUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(IGetRenderingHtmlRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly RenderingHTMLUrlBuilder urlBuilder;
  }
}

