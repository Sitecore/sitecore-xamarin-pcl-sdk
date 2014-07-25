
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Validators;

  public class UpdateItemByIdUrlBuilder : AbstractUpdateItemUrlBuilder<IUpdateItemByIdRequest>
  {
    public UpdateItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    //TODO: igk common part for all requests "by id", we should merge it somehow!!!
    protected override string GetSpecificPartForRequest(IUpdateItemByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId).ToLowerInvariant();

      string result = 
          this.restGrammar.FieldSeparator 
        + this.webApiGrammar.ItemIdParameterName
        + this.restGrammar.KeyValuePairSeparator
        + escapedId;

      return result;
    }

    protected override void ValidateSpecificRequest(IUpdateItemByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId);
    }
  }
}

