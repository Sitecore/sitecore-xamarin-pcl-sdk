namespace Sitecore.MobileSDK.UrlBuilder.Search
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class RunSitecoreSearchUrlBuilder : GetPagedItemsUrlBuilder<ISitecoreSearchRequest>
  {
    public RunSitecoreSearchUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    protected override string GetHostUrlForRequest(ISitecoreSearchRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);

      string result = hostUrl + this.restGrammar.PathComponentSeparator + sscGrammar.ItemSearchAction;

      return result;
    }

    protected override string GetItemIdenticationForRequest(ISitecoreSearchRequest request)
    {
      string strItemPath = this.sscGrammar.SitecoreSearchParameterName + this.restGrammar.KeyValuePairSeparator + request.Term;
      return strItemPath;

    }

    protected override void ValidateSpecificRequest(ISitecoreSearchRequest request)
    {
      base.ValidateSpecificRequest(request);
      ItemIdValidator.ValidateSearchRequest(request.Term, this.GetType().Name + ".Term");
    }
  }
}
