namespace Sitecore.MobileSDK.API.Request
{
  public interface IReadItemsByQueryRequest : IBaseReadItemsRequest
  {
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    string SitecoreQuery { get; }
  }
}
