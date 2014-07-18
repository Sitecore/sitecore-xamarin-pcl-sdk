namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder;

    public interface IReadItemsByPathRequest : IBaseGetItemRequest
  {
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    string ItemPath { get; }
  }
}
