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

    protected override string GetHostUrlForRequest(ICreateItemByPathRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());

      string result = hostUrl + escapedPath;
      return result;
    }

    protected override void ValidateSpecificRequest(ICreateItemByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.ItemPath, this.GetType().Name + ".ItemPath");
    }
  }
}

