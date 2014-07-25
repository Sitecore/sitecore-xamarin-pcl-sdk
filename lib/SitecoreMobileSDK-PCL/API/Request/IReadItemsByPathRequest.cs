namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder;

  public interface IReadItemsByPathRequest : IBaseItemRequest
  {
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    string ItemPath { get; }
  }
}
