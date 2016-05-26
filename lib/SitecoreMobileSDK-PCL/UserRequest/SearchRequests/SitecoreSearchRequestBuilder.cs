using Sitecore.MobileSDK.API.Request.Parameters;

namespace Sitecore.MobileSDK.UserRequest.SearchRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class SitecoreSearchRequestBuilder : AbstractGetVersionedItemRequestBuilder<SitecoreSearchParameters>
  {
    public SitecoreSearchRequestBuilder(string term)
    {
      ItemIdValidator.ValidateSearchRequest(term, this.GetType().Name + ".term");

      this.term = term;
    }

    public override SitecoreSearchParameters Build()
    {
      IPagingParameters pagingSettings = this.AccumulatedPagingParameters;

      SitecoreSearchParameters result = new SitecoreSearchParameters(
        null, 
        this.itemSourceAccumulator, 
        this.queryParameters, 
        pagingSettings,
        this.term);

      return result;
    }

    private readonly string term;
  }
}

