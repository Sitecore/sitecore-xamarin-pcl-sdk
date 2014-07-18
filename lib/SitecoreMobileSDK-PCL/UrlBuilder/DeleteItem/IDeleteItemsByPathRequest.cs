namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public interface IDeleteItemsByPathRequest : IBaseDeleteItemRequest
  {
    string ItemPath { get; }
  }
}
