
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public class CreateItemByPathUrlBuilder : AbstractCreateItemUrlBuilder<ICreateItemByPathRequest>
  {
    public CreateItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
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
      ItemPathValidator.ValidateItemPath(request.ItemPath);
    }
  }
}

