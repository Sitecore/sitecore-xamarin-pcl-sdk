namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder;

    public interface IReadItemsByQueryRequest : IBaseItemRequest
  {
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    string SitecoreQuery { get; }
  }
}
