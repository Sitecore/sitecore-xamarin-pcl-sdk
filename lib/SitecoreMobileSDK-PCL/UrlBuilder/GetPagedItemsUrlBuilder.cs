namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;


  public abstract class GetPagedItemsUrlBuilder<TRequest> : AbstractGetItemUrlBuilder<TRequest>
    where TRequest : IBaseReadItemsRequest
  {
    public GetPagedItemsUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar) : 
    base(restGrammar, sscGrammar)
    {
    }

    protected override string GetSpecificPartForRequest(TRequest request)
    {
      string formattedItemPath = this.GetItemIdenticationForRequest(request);
      string formattedPagingInfo = this.GetPagingInfoForRequest(request);

      string result = null;
      if (string.IsNullOrEmpty(formattedItemPath))
      {
        result = formattedPagingInfo;
      }
      else
      {
        result = formattedItemPath;

        if (!string.IsNullOrEmpty(formattedPagingInfo))
        {
          result = result + this.restGrammar.FieldSeparator + formattedPagingInfo;
        }
      }

      return result;
    }

    private string GetPagingInfoForRequest(TRequest request)
    {
      var pageBuilder = new PagingUrlBuilder(this.restGrammar, this.sscGrammar);
      string strPageInfo = pageBuilder.BuildUrlQueryString(request.PagingSettings);

      return strPageInfo;
    }

    protected abstract string GetItemIdenticationForRequest(TRequest request);


    protected override void ValidateSpecificRequest(TRequest request)
    {
      var pagingSettings = request.PagingSettings;
      if (null == pagingSettings)
      {
        return;
      }

      bool isPageNumberValid = ( pagingSettings.PageNumber >= 0 );
      bool isItemsPerPageCountValid = ( pagingSettings.ItemsPerPageCount > 0 );

      bool isSettingsValid = isPageNumberValid && isItemsPerPageCountValid;
      if (!isSettingsValid)
      {
        string message = string.Format(
          "Incorrect paging settings. [ Page #{0} | Page Size {1} ]", 
          pagingSettings.PageNumber, 
          pagingSettings.ItemsPerPageCount);

        throw new ArgumentException(message);
      }
    }
  }
}

