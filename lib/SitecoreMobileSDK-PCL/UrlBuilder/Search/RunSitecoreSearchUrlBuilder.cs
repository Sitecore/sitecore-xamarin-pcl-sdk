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

    protected override string GetSpecificPartForRequest(ISitecoreSearchRequest request)
    {
      string baseParams = base.GetSpecificPartForRequest(request);

      string sortValue = "";

      foreach (string field in request.SortParameters.Fields) {
        sortValue += "|" + field;
      }

      if (sortValue.Length > 0) {
        baseParams += this.restGrammar.FieldSeparator
                          + this.sscGrammar.SortingParameterName
                          + this.restGrammar.KeyValuePairSeparator
                          + sortValue;
      }

      return baseParams;
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
