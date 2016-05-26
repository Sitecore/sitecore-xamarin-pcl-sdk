namespace Sitecore.MobileSDK.UrlBuilder.Search
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class RunStoredSearchUrlBuilder : GetPagedItemsUrlBuilder<ISitecoreStoredSearchRequest>
  {
    public RunStoredSearchUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    protected override string GetHostUrlForRequest(ISitecoreStoredSearchRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string itemId = UrlBuilderUtils.EscapeDataString(request.ItemId.ToLowerInvariant());

      string result = hostUrl + this.restGrammar.PathComponentSeparator + itemId + sscGrammar.RunStoredSearchAction;
      return result;
    }

    protected override string GetItemIdenticationForRequest(ISitecoreStoredSearchRequest request)
    {
      string strItemPath = this.sscGrammar.SitecoreSearchParameterName + this.restGrammar.KeyValuePairSeparator + request.Term;
      return strItemPath;

    }

    protected override void ValidateSpecificRequest(ISitecoreStoredSearchRequest request)
    {
      base.ValidateSpecificRequest(request);
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}
