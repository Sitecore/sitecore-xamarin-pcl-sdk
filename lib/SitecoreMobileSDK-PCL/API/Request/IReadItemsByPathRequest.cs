namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.UrlBuilder;

  public interface IReadItemsByPathRequest : IBaseReadItemsRequest
  {
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    string ItemPath { get; }
  }
}
