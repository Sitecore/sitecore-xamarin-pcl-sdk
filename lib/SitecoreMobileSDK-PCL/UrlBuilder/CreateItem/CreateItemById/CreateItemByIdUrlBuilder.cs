
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;

  public class CreateItemByIdUrlBuilder : AbstractCreateItemUrlBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    protected override string GetSpecificPartForRequest(ICreateItemByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId).ToLowerInvariant();

      string result = base.GetSpecificPartForRequest(request);
      result += this.restGrammar.FieldSeparator 
        + this.webApiGrammar.ItemIdParameterName
        + this.restGrammar.KeyValuePairSeparator
        + escapedId;

      return result;
    }

    protected override void ValidateSpecificRequest(ICreateItemByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId);
    }
  }
}

