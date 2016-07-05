using System;
using Sitecore.MobileSDK.API.Request;
using Sitecore.MobileSDK.API.Request.Parameters;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.UserRequest.SearchRequest;
using Sitecore.MobileSDK.Validators;

namespace Sitecore.MobileSDK
{
  public class SitecoreSearchRequestBuilder : AbstractSitecoreSearchRequestBuilder<SitecoreSearchParameters>
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
        this.sortParameters,
        this.term);

      return result;
    }
  }
}

