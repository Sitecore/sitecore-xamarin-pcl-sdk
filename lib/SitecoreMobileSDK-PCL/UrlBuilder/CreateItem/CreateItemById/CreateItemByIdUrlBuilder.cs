

namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;

  public class CreateItemByIdUrlBuilder : AbstractGetItemUrlBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    protected override string GetSpecificPartForRequest(ICreateItemByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);
      string escapedTemplate = UrlBuilderUtils.EscapeDataString(request.ItemTemplate);

      string result = this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedId;
      result += this.restGrammar.FieldSeparator + this.webApiGrammar.TemplateParameterName + this.restGrammar.KeyValuePairSeparator + escapedTemplate;

      return result.ToLowerInvariant();
    }

    protected override void ValidateSpecificRequest(ICreateItemByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId);
    }
  }
}

