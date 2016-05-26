namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ItemByQueryUrlBuilder : GetPagedItemsUrlBuilder<IReadItemsByQueryRequest>
  {
    public ItemByQueryUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    protected override string GetItemIdenticationForRequest(IReadItemsByQueryRequest request)
    {
      string escapedQuery = UrlBuilderUtils.EscapeDataString(request.SitecoreQuery);
      string formattedQuery = this.sscGrammar.SitecoreQueryParameterName + this.restGrammar.KeyValuePairSeparator + escapedQuery;

      return formattedQuery;
    }

    protected override void ValidateSpecificRequest(IReadItemsByQueryRequest request)
    {
      base.ValidateSpecificRequest(request);

      SitecoreQueryValidator.ValidateSitecoreQuery(request.SitecoreQuery, this.GetType().Name + ".SitecoreQuery");
    }
  }
}

