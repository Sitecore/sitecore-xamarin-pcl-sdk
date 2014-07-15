namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
  public interface IReadItemsByQueryRequest : IBaseItemRequest
  {
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    string SitecoreQuery { get; }
  }
}
