namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ItemByPathUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByPathRequest>
  {
    public ItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetHostUrlForRequest(IReadItemsByPathRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());

      string result = hostUrl + escapedPath;
      return result;
    }

    protected override string GetSpecificPartForRequest(IReadItemsByPathRequest request)
    {
      var pageBuilder = new PagingUrlBuilder(this.restGrammar, this.webApiGrammar);
      string strPageInfo = pageBuilder.BuildUrlQueryString(request.PagingSettings);

      return strPageInfo;
    }

    protected override void ValidateSpecificRequest(IReadItemsByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.ItemPath, this.GetType().Name + ".ItemPath");
    }
  }
}
