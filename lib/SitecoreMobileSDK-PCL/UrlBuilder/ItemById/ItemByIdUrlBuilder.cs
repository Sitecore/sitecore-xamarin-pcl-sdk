namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ItemByIdUrlBuilder : GetPagedItemsUrlBuilder<IReadItemsByIdRequest>
  {
    public ItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetItemIdenticationForRequest(IReadItemsByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);
      string strItemId = this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedId;
      string lowerCaseItemId = strItemId.ToLowerInvariant();

      return lowerCaseItemId;
    }

    protected override void ValidateSpecificRequest(IReadItemsByIdRequest request)
    {
      base.ValidateSpecificRequest(request);
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}
