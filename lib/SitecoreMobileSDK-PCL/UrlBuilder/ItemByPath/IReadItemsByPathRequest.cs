namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
  public interface IReadItemsByPathRequest : IBaseItemRequest
  {
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    string ItemPath { get; }
  }
}
