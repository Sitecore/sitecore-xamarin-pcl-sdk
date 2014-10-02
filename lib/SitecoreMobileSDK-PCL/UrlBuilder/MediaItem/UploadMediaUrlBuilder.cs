using Sitecore.MobileSDK.API.Items;


namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;

  public class UploadMediaUrlBuilder
  {
    public UploadMediaUrlBuilder(
      IRestServiceGrammar restGrammar,
      IWebApiUrlParameters webApiGrammar,
      ISessionConfig sessionConfig,
      IMediaLibrarySettings mediaSettings
    )
    {
      this.mediaSettings = mediaSettings;
      this.restGrammar = restGrammar;
      this.mediaSettings = mediaSettings;
      this.webApiGrammar = webApiGrammar;
    }
      
    public string GetUrlForRequest(IMediaResourceUploadRequest request)
    {
      return "";
    }

    private IWebApiUrlParameters webApiGrammar;
    private IMediaLibrarySettings mediaSettings;
    private IRestServiceGrammar restGrammar;
    private ISessionConfig sessionConfig;
  }
}

