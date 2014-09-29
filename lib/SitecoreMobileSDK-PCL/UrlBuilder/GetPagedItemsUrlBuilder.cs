using Sitecore.MobileSDK.API.Request;
using Sitecore.MobileSDK.UrlBuilder.Rest;
using Sitecore.MobileSDK.UrlBuilder.WebApi;

namespace Sitecore.MobileSDK.UrlBuilder
{
  public abstract class GetPagedItemsUrlBuilder<TRequest> : AbstractGetItemUrlBuilder<TRequest>
    where TRequest : IBaseReadItemsRequest
  {
    public GetPagedItemsUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar) : 
    base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetSpecificPartForRequest(TRequest request)
    {
      string formattedItemId = this.GetItemIdenticationForRequest(request);
      string formattedPagingInfo = this.GetPagingInfoForRequest(request);

      string result = null;
      if (string.IsNullOrEmpty(formattedItemId))
      {
        result = formattedPagingInfo;
      }
      else
      {
        result = formattedItemId;

        if (!string.IsNullOrEmpty(formattedPagingInfo))
        {
          result = result + this.restGrammar.FieldSeparator + formattedPagingInfo;
        }
      }

      return result;
    }

    private string GetPagingInfoForRequest(TRequest request)
    {
      var pageBuilder = new PagingUrlBuilder(this.restGrammar, this.webApiGrammar);
      string strPageInfo = pageBuilder.BuildUrlQueryString(request.PagingSettings);

      return strPageInfo;
    }

    protected abstract string GetItemIdenticationForRequest(TRequest request);


    protected override void ValidateSpecificRequest(TRequest request)
    {
      //IDLE
    }
  }
}

