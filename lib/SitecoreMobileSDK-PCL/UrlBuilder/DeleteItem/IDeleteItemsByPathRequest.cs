namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public interface IDeleteItemsByPathRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByPathRequest DeepCopyDeleteItemRequest();
    string ItemPath { get; }
  }
}
