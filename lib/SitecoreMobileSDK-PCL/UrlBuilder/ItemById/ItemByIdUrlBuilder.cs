namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ItemByIdUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByIdRequest>
  {
    public ItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetSpecificPartForRequest(IReadItemsByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);
      string strItemId = this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedId;
      string lowerCaseItemId = strItemId.ToLowerInvariant();

      var pageBuilder = new PagingUrlBuilder(this.restGrammar, this.webApiGrammar);
      string strPageInfo = pageBuilder.BuildUrlQueryString(request.PagingSettings);

      string result = lowerCaseItemId;
      if (!string.IsNullOrEmpty(strPageInfo))
      {
        result = result + this.restGrammar.FieldSeparator + strPageInfo;
      }

      return result;
    }

    protected override void ValidateSpecificRequest(IReadItemsByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}
