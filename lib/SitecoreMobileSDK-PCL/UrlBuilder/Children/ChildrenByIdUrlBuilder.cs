namespace Sitecore.MobileSDK.UrlBuilder.Children
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ChildrenByIdUrlBuilder : GetPagedItemsUrlBuilder<IReadItemsByIdRequest>
  {
    public ChildrenByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    protected override string GetHostUrlForRequest(IReadItemsByIdRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string itemId = UrlBuilderUtils.EscapeDataString(request.ItemId.ToLowerInvariant());

      string result = hostUrl + this.restGrammar.PathComponentSeparator + itemId + webApiGrammar.ItemWebApiChildrenAction;
      return result;
    }

    protected override string GetItemIdenticationForRequest(IReadItemsByIdRequest request)
    {
      return null;
    }

    protected override void ValidateSpecificRequest(IReadItemsByIdRequest request)
    {
      base.ValidateSpecificRequest(request);
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}
