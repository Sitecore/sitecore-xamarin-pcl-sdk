namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder;

    public interface IReadItemsByQueryRequest : IBaseGetItemRequest
  {
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    string SitecoreQuery { get; }
  }
}
