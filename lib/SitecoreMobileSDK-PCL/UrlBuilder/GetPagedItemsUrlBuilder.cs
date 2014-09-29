using Sitecore.MobileSDK.API.Request;
using Sitecore.MobileSDK.UrlBuilder.Rest;
using Sitecore.MobileSDK.UrlBuilder.WebApi;

namespace Sitecore.MobileSDK.UrlBuilder
{
  public class GetPagedItemsUrlBuilder<TRequest> : AbstractGetItemUrlBuilder<TRequest>
    where TRequest : IBaseReadItemsRequest
  {
    public GetPagedItemsUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar) : 
    base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetSpecificPartForRequest(TRequest request)
    {
      var pageBuilder = new PagingUrlBuilder(this.restGrammar, this.webApiGrammar);
      string strPageInfo = pageBuilder.BuildUrlQueryString(request.PagingSettings);

      return strPageInfo;
    }

    protected override void ValidateSpecificRequest(TRequest request)
    {
      //IDLE
    }
  }
}

