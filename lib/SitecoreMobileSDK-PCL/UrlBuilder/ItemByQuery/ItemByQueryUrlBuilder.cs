﻿
namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ItemByQueryUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByQueryRequest>
  {
    public ItemByQueryUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetSpecificPartForRequest(IReadItemsByQueryRequest request)
    {
      this.ValidateRequest(request);
      string escapedQuery = UrlBuilderUtils.EscapeDataString(request.SitecoreQuery);
      string result = this.webApiGrammar.SitecoreQueryParameterName + this.restGrammar.KeyValuePairSeparator + escapedQuery;

      return result;
    }

    protected override void ValidateSpecificRequest(IReadItemsByQueryRequest request)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(request.SitecoreQuery, this.GetType().Name + ".SitecoreQuery");
    }
  }
}

