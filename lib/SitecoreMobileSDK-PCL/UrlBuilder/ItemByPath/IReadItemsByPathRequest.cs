namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
  public interface IReadItemsByPathRequest : IBaseGetItemRequest
  {
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    string ItemPath { get; }
  }
}
