namespace Sitecore.MobileSDK.UrlBuilder.Search
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class RunSitecoreSearchUrlBuilder : GetPagedItemsUrlBuilder<ISitecoreSearchRequest>
  {
    public RunSitecoreSearchUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetHostUrlForRequest(ISitecoreSearchRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);

      string result = hostUrl + this.restGrammar.PathComponentSeparator + webApiGrammar.ItemSearchAction;

      return result;
    }

    protected override string GetItemIdenticationForRequest(ISitecoreSearchRequest request)
    {
      string strItemPath = this.webApiGrammar.SitecoreSearchParameterName + this.restGrammar.KeyValuePairSeparator + request.Term;
      return strItemPath;

    }

    protected override void ValidateSpecificRequest(ISitecoreSearchRequest request)
    {
      base.ValidateSpecificRequest(request);
      ItemIdValidator.ValidateSearchRequest(request.Term, this.GetType().Name + ".Term");
    }
  }
}
