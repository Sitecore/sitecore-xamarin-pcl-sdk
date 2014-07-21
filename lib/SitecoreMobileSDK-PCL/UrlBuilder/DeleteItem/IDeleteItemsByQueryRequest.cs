namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public interface IDeleteItemsByQueryRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest();
    string SitecoreQuery { get; }
  }
}
