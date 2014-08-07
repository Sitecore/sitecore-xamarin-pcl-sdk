namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.UrlBuilder;

  public interface IReadItemsByQueryRequest : IBaseReadItemsRequest
  {
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    string SitecoreQuery { get; }
  }
}
