namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Validators;

  public class CreateItemByPathUrlBuilder : AbstractCreateItemUrlBuilder<ICreateItemByPathRequest>
  {
    public CreateItemByPathUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    public override string GetUrlForRequest(ICreateItemByPathRequest request)
    {
      this.Validate(request);

      string hostUrl = base.GetBaseUrlForRequest(request);

      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());

      string result = hostUrl + escapedPath;

      return result.ToLowerInvariant();
    }

    public override void ValidateSpecificPart(ICreateItemByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.ItemPath, this.GetType().Name + ".ItemPath");
    }
  }
}

