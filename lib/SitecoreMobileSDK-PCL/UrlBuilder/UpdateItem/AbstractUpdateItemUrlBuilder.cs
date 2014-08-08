namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ChangeItem;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public abstract class AbstractUpdateItemUrlBuilder<TRequest> : AbstractChangeItemUrlBuilder<TRequest>
    where TRequest : IBaseUpdateItemRequest
  {
    public AbstractUpdateItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }
  }
}

