namespace Sitecore.MobileSDK.API.Request
{
  public interface IDeleteItemsByQueryRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest();
    string SitecoreQuery { get; }
  }
}
