namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
  public interface IReadItemsByQueryRequest : IBaseGetItemRequest
  {
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    string SitecoreQuery { get; }
  }
}
