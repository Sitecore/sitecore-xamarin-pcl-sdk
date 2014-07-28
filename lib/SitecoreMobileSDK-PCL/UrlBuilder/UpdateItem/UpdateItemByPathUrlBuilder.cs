
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API.Request;

  public class UpdateItemByPathUrlBuilder : AbstractUpdateItemUrlBuilder<IUpdateItemByPathRequest>
  {
    public UpdateItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    protected override string GetSpecificPartForRequest(IUpdateItemByPathRequest request)
    {
      return string.Empty;
    }

    protected override string GetHostUrlForRequest(IUpdateItemByPathRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());

      string result = hostUrl + escapedPath;
      return result;
    }

    protected override void ValidateSpecificRequest(IUpdateItemByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.ItemPath);
    }
   
  }
}

